#region

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using log4net;
using System.Text;
using wServer.networking.svrPackets;
using System.Threading;

#endregion

namespace wServer.networking
{
    //hackish code
    internal class NetworkHandler : IDisposable
    {

        public const int MEMORY_USAGE_DEFAULT = 4096;

        public const int MEMORY_USAGE_TINY = 5120;//less 5 connections
        public const int MEMORY_USAGE_MEDIUM = 7168;//less 9 connections
        public const int MEMORY_USAGE_BIG = 9216;//less 23 connections
        public const int MEMORY_USAGE_LARGE = 16384;//less 31 connections
        public const int MEMORY_USAGE_HUGE_1 = 19456;//less 31 connections
        public const int MEMORY_USAGE_HUGE_2 = 23552;//less 45 connections
        public const int MEMORY_USAGE_HUGE_3 = 26624;//less 65 connections
        public const int MEMORY_USAGE_HUGE_4 = 32768;//less 100 connections
        public const int MEMORY_USAGE_HUGE_TEST = 65535;
        public const int MEMORY_USAGE_HUGE_TEST_2 = 100000;

        private bool disposed;
 		    private bool disposeCalled;
 		    private int m_recvOperating;
 		    private int m_sendOperating;

        public const int BUFFER_SIZE = int.MaxValue / MEMORY_USAGE_HUGE_4;
        private static readonly ILog log = LogManager.GetLogger(typeof(NetworkHandler));
        private readonly Client parent;
        private readonly ConcurrentQueue<Packet> pendingPackets = new ConcurrentQueue<Packet>();
        private readonly object sendLock = new object();
        private readonly Socket skt;

        private SocketAsyncEventArgs receive;
        private byte[] receiveBuff;
        private ReceiveState receiveState = ReceiveState.Awaiting;

        private SocketAsyncEventArgs send;
        private byte[] sendBuff;
        private SendState sendState = SendState.Awaiting;

        public NetworkHandler(Client parent, Socket skt)
        {
            this.parent = parent;
            this.skt = skt;
        }

        public void BeginHandling()
        {
            skt.NoDelay = true;
            skt.UseOnlyOverlappedIO = true;

            send = new SocketAsyncEventArgs();
            send.Completed += SendCompleted;
            send.UserToken = new SendToken();
            send.SetBuffer(sendBuff = new byte[BUFFER_SIZE], 0, BUFFER_SIZE);

            receive = new SocketAsyncEventArgs();
            receive.Completed += ReceiveCompleted;
            receive.UserToken = new ReceiveToken();
            receive.SetBuffer(receiveBuff = new byte[BUFFER_SIZE], 0, BUFFER_SIZE);

            receiveState = ReceiveState.ReceivingHdr;
            receive.SetBuffer(0, 5);
            if (!skt.ReceiveAsync(receive))
                ReceiveCompleted(this, receive);
        }

        private void ProcessPolicyFile() //WUT.
        {
            NetworkStream s = new NetworkStream(skt);
            NWriter wtr = new NWriter(s);
            wtr.WriteNullTerminatedString(@"<cross-domain-policy>
     <allow-access-from domain=""*"" to-ports=""*"" />
</cross-domain-policy>");
            wtr.Write((byte)'\r');
            wtr.Write((byte)'\n');
            parent.Disconnect();
        }

        //It is said that ReceiveAsync/SendAsync never returns false unless error
        //So...let's just treat it as always true

        private void ReceiveCompleted(object sender, SocketAsyncEventArgs e)
        {
 			m_recvOperating = 0;
 
 			if (disposeCalled)
 				Dispose();
            try
            {
                if (!skt.Connected)
                {
                    parent.Disconnect();
                    return;
                }

                if (e.SocketError != SocketError.Success)
                    throw new SocketException((int)e.SocketError);

                switch (receiveState)
                {
                    case ReceiveState.ReceivingHdr:
                        if (e.BytesTransferred < 5)
                        {
                            parent.Disconnect();
                            return;
                        }

                        if (e.Buffer[0] == 0x4d && e.Buffer[1] == 0x61 &&
                            e.Buffer[2] == 0x64 && e.Buffer[3] == 0x65 && e.Buffer[4] == 0xff)
                        {
                            //log.InfoFormat("Usage request from: @ {0}.", skt.RemoteEndPoint);
                            byte[] c = Encoding.ASCII.GetBytes(parent.Manager.MaxClients +
                                ":" + parent.Manager.Clients.Count.ToString());
                            skt.Send(c);
                            return;
                        }

                        if (e.Buffer[0] == 0x3c && e.Buffer[1] == 0x70 &&
                            e.Buffer[2] == 0x6f && e.Buffer[3] == 0x6c && e.Buffer[4] == 0x69)
                        {
                            ProcessPolicyFile();
                            return;
                        }

                        int len = (e.UserToken as ReceiveToken).Length =
                            IPAddress.NetworkToHostOrder(BitConverter.ToInt32(e.Buffer, 0)) - 5;
                        if (len < 0 || len > BUFFER_SIZE)
                            throw new InternalBufferOverflowException();
                        Packet packet = null;
                        try
                        {
                            packet = Packet.Packets[(PacketID)e.Buffer[4]].CreateInstance();
                        }
                        catch
                        {
                            log.ErrorFormat("Packet ID not found: {0}", e.Buffer[4]);
                        }
                        (e.UserToken as ReceiveToken).Packet = packet;

                        receiveState = ReceiveState.ReceivingBody;
                        e.SetBuffer(0, len);
                        skt.ReceiveAsync(e);
                        break;
                    case ReceiveState.ReceivingBody:
                        if (e.BytesTransferred < (e.UserToken as ReceiveToken).Length)
                        {
                            parent.Disconnect();
                            return;
                        }

                        Packet pkt = (e.UserToken as ReceiveToken).Packet;
                        pkt.Read(parent, e.Buffer, 0, (e.UserToken as ReceiveToken).Length);

                        receiveState = ReceiveState.Processing;
                        bool cont = OnPacketReceived(pkt);

                        if (cont && skt.Connected)
                        {
                            receiveState = ReceiveState.ReceivingHdr;
                            e.SetBuffer(0, 5);
                            skt.ReceiveAsync(e);
                        }
                        break;
                    default:
                        throw new InvalidOperationException(e.LastOperation.ToString());
                }
            }
            catch// (Exception ex)
            {
                //OnError(ex);
            }
        }

        private void SendCompleted(object sender, SocketAsyncEventArgs e)
        {
 			m_sendOperating = 0;
 
 			if (disposeCalled)
 				Dispose();
            try
            {
                if (!skt.Connected || disposed) return;

                int len;
                switch (sendState)
                {
                    case SendState.Ready:
                        len = (e.UserToken as SendToken).Packet.Write(parent, sendBuff, 0);

                        sendState = SendState.Sending;
                        e.SetBuffer(0, len);

                        if (!skt.Connected) return;
                        skt.SendAsync(e);
                        break;
                    case SendState.Sending:
                        (e.UserToken as SendToken).Packet = null;

                        if (CanSendPacket(e, true))
                        {
                            len = (e.UserToken as SendToken).Packet.Write(parent, sendBuff, 0);

                            sendState = SendState.Sending;
                            e.SetBuffer(0, len);

                            if (!skt.Connected) return;
                            skt.SendAsync(e);
                        }
                        break;
                }
            }
            catch// (Exception ex)
            {
                //OnError(ex);
            }
        }


        private void OnError(Exception ex)
        {
            log.Error("Socket error.", ex);
            parent.Disconnect();
        }

        private bool OnPacketReceived(Packet pkt)
        {
            if (parent.IsReady())
            {
                parent.Manager.Network.AddPendingPacket(parent, pkt);
                return true;
            }
            return false;
        }

        private bool CanSendPacket(SocketAsyncEventArgs e, bool ignoreSending)
        {
            lock (sendLock)
            {
                if (sendState == SendState.Ready ||
                    (!ignoreSending && sendState == SendState.Sending))
                    return false;
                Packet packet;
                if (pendingPackets.TryDequeue(out packet))
                {
                    (e.UserToken as SendToken).Packet = packet;
                    sendState = SendState.Ready;
                    return true;
                }
                sendState = SendState.Awaiting;
                return false;
            }
        }

        public void SendPacket(Packet pkt)
        {
            if (!skt.Connected) return;
            pendingPackets.Enqueue(pkt);
            if (CanSendPacket(send, false))
            {
                int len = (send.UserToken as SendToken).Packet.Write(parent, sendBuff, 0);

                sendState = SendState.Sending;
                send.SetBuffer(sendBuff, 0, len);
                if (!skt.SendAsync(send))
                    SendCompleted(this, send);
            }
        }

        public void SendPackets(IEnumerable<Packet> pkts)
        {
            if (!skt.Connected) return;
            foreach (Packet i in pkts)
                pendingPackets.Enqueue(i);
            if (CanSendPacket(send, false))
            {
                int len = (send.UserToken as SendToken).Packet.Write(parent, sendBuff, 0);

                sendState = SendState.Sending;
                send.SetBuffer(sendBuff, 0, len);
                if (!skt.SendAsync(send))
                    SendCompleted(this, send);
            }
        }

        private enum ReceiveState
        {
            Awaiting,
            ReceivingHdr,
            ReceivingBody,
            Processing
        }

        private class ReceiveToken
        {
            public int Length { get; set; }
            public Packet Packet { get; set; }
        }

        private enum SendState
        {
            Awaiting,
            Ready,
            Sending
        }

        private class SendToken
        {
            public Packet Packet;
        }

        public void Dispose()
        {
            //send.Completed -= SendCompleted;
            //send.Dispose();
            //sendBuff = null;
            //receive.Completed -= ReceiveCompleted;
            //receive.Dispose();
            //receiveBuff = null;
 			disposeCalled = true;
 			if (Interlocked.Exchange(ref m_sendOperating, 1) != 0 || Interlocked.Exchange(ref m_recvOperating, 1) != 0 || disposed)
 				return;
 			disposed = true;
 			if (send != null)
 			{
 				send.Completed -= SendCompleted;
 				send.Dispose();
 				sendBuff = null;
 			}
 			if (receive != null)
 			{
 				receive.Completed -= ReceiveCompleted;
 				receive.Dispose();
 				receiveBuff = null;
 			}
 			sendBuff = null;
 			receiveBuff = null;
 			send = null;
 			receive = null;
 			skt?.Dispose();
        }
    }
}
