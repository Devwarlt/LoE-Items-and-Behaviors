#region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
#endregion

namespace Rextester
{
    public class Program
    {
        //Test it on http://rextester.com/
        //hackish code used to know about buffer lenght used on VPS with basic specs average RAM: 3GB to 8GB.
        public static void Main(string[] args)
        {
            int buffer = 0xffffff;
            int newBuffer = 0;
            int i;
            int limit = 20;
            string data_ = "";
            
            Console.WriteLine("NETWORK BUFFER FOR WSERVER.EXE - LOG:\n");
            
            for (i = 1; i <= limit; i++) {
                if (i < limit) {
                    newBuffer = buffer / i;
                    var formula = newBuffer * 2 / (1024 * 1024);
                    data_ = i + ") " + newBuffer + " network buffer consume " + formula + "MB memory when new client connect;";
                    Console.WriteLine(data_);
                } else {
                    newBuffer = buffer / limit;
                    var formula = newBuffer * 2 / (1024 * 1024);
                    data_ = limit + ") " + newBuffer + " network buffer consume " + formula + "MB memory when new client connect.";
                    Console.WriteLine(data_);
                }
            }
            
            Console.WriteLine("\nCredits:\n- Devwarlt.");
            
        }
    }
}
