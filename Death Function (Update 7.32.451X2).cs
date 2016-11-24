
        public void Death(XmlData data, Account acc, Char chr, string killer) //Save first
        {
            MySqlCommand cmd = CreateQuery();
//            cmd.CommandText = @"UPDATE characters SET 
//dead=TRUE, 
//deathTime=NOW() 
//WHERE accId=@accId AND charId=@charId;";
//            cmd.Parameters.AddWithValue("@accId", acc.AccountId);
//            cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
//            cmd.ExecuteNonQuery();

            bool firstBorn;
            int finalFame = chr.FameStats.CalculateTotal(data, acc, chr, chr.CurrentFame, out firstBorn);
            Random randomize = new Random();

            int invDeath = 0;
            int backpackDeath = 0;

            if((chr.Level > 20) && (chr.Level <= 50))
            {
                invDeath = randomize.Next(100);
                backpackDeath = randomize.Next(75);
            }
            else if((chr.Level > 50) && (chr.Level <= 100))
            {
                invDeath = randomize.Next(75);
                backpackDeath = randomize.Next(50);
            }
            else if((chr.Level > 100) && (chr.Level <= 150))
            {
                invDeath = randomize.Next(50);
                backpackDeath = randomize.Next(25);
            }
            else if((chr.Level > 150) && (chr.Level <= 200))
            {
                invDeath = randomize.Next(25);
                backpackDeath = randomize.Next(15);
            }
            else if(chr.Level > 200)
            {
                invDeath = randomize.Next(15);
                backpackDeath = randomize.Next(10);
            }

            if(acc.Rank == 1)
            {
//                update characters set 
//items=replace(items,substring_index(items,',',-8),'-1, -1, -1, -1, -1, -1, -1, -1')
//where charId=1;
                cmd.CommandText = @"UPDATE characters SET  
level=1,
hp=100,
mp=100,
stats='100, 100, 0, 0, 0, 0, 0, 0',
tex1=0,
tex2=0,
exp=exp-(0.1*exp),
fame=fame-(0.25*fame),
deathTime=NOW() 
WHERE accId=@accId AND charId=@charId;";
                cmd.Parameters.AddWithValue("@accId", acc.AccountId);
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();

                cmd = CreateQuery();
                cmd.CommandText = @"UPDATE stats SET 
                fame=fame+(2*@amount), 
                totalFame=totalFame+(2*@amount)
                WHERE accId=@accId;";
                cmd.Parameters.AddWithValue("@accId", acc.AccountId);
                cmd.Parameters.AddWithValue("@amount", finalFame);
                cmd.ExecuteNonQuery();
            } else
            {
                cmd.CommandText = @"UPDATE characters SET  
level=1,
hp=100,
mp=100,
stats='100, 100, 0, 0, 0, 0, 0, 0',
tex1=0,
tex2=0,
exp=exp-(0.2*exp),
fame=fame-(0.25*fame),
deathTime=NOW() 
WHERE accId=@accId AND charId=@charId;";
                cmd.Parameters.AddWithValue("@accId", acc.AccountId);
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();

                cmd = CreateQuery();
                cmd.CommandText = @"UPDATE stats SET 
                fame=fame+@amount, 
                totalFame=totalFame+@amount 
                WHERE accId=@accId;";
                cmd.Parameters.AddWithValue("@accId", acc.AccountId);
                cmd.Parameters.AddWithValue("@amount", finalFame);
                cmd.ExecuteNonQuery();
            }
            if(invDeath == 1)
            {
                cmd.CommandText = @"UPDATE characters SET 
items=replace(items,substring_index(items,',',-1),' -1') 
where charId=@charId;";
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();
            }
            else if(invDeath == 2) {
                cmd.CommandText = @"UPDATE characters SET 
items=replace(items,substring_index(items,',',-2),' -1, -1') 
where charId=@charId;";
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();
            }
            else if(invDeath == 3) {
                cmd.CommandText = @"UPDATE characters SET 
items=replace(items,substring_index(items,',',-3),' -1, -1, -1') 
where charId=@charId;";
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();
            }
            else if(invDeath == 4) {
                cmd.CommandText = @"UPDATE characters SET 
items=replace(items,substring_index(items,',',-4),' -1, -1, -1, -1') 
where charId=@charId;";
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();
            }
            else if(invDeath == 5) {
                cmd.CommandText = @"UPDATE characters SET 
items=replace(items,substring_index(items,',',-5),' -1, -1, -1, -1, -1') 
where charId=@charId;";
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();
            }
            else if(invDeath == 6) {
                cmd.CommandText = @"UPDATE characters SET 
items=replace(items,substring_index(items,',',-6),' -1, -1, -1, -1, -1, -1') 
where charId=@charId;";
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();
            }
            else if(invDeath == 7) {
                cmd.CommandText = @"UPDATE characters SET 
items=replace(items,substring_index(items,',',-7),' -1, -1, -1, -1, -1, -1, -1') 
where charId=@charId;";
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();
            }
            else if(invDeath == 8) {
                cmd.CommandText = @"UPDATE characters SET 
items=replace(items,substring_index(items,',',-8),' -1, -1, -1, -1, -1, -1, -1, -1') 
where charId=@charId;";
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();
            }
            if(chr.HasBackpack == 1)
            {
            if(backpackDeath == 1)
            {
                cmd.CommandText = @"UPDATE backpacks SET 
items=replace(items,substring_index(items,',',-1),' -1') 
where charId=@charId;";
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();
            }
            else if(backpackDeath == 2) {
                cmd.CommandText = @"UPDATE backpacks SET 
items=replace(items,substring_index(items,',',-2),' -1, -1') 
where charId=@charId;";
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();
            }
            else if(backpackDeath == 3) {
                cmd.CommandText = @"UPDATE backpacks SET 
items=replace(items,substring_index(items,',',-3),' -1, -1, -1') 
where charId=@charId;";
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();
            }
            else if(backpackDeath == 4) {
                cmd.CommandText = @"UPDATE backpacks SET 
items=replace(items,substring_index(items,',',-4),' -1, -1, -1, -1') 
where charId=@charId;";
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();
            }
            else if(backpackDeath == 5) {
                cmd.CommandText = @"UPDATE backpacks SET 
items=replace(items,substring_index(items,',',-5),' -1, -1, -1, -1, -1') 
where charId=@charId;";
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();
            }
            else if(backpackDeath == 6) {
                cmd.CommandText = @"UPDATE backpacks SET 
items=replace(items,substring_index(items,',',-6),' -1, -1, -1, -1, -1, -1') 
where charId=@charId;";
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();
            }
            else if(backpackDeath == 7) {
                cmd.CommandText = @"UPDATE backpacks SET 
items=replace(items,substring_index(items,',',-7),' -1, -1, -1, -1, -1, -1, -1') 
where charId=@charId;";
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();
            }
            else if(backpackDeath == 8) {
                cmd.CommandText = @"UPDATE backpacks SET 
items=replace(items,substring_index(items,',',-8),' -1, -1, -1, -1, -1, -1, -1, -1') 
where charId=@charId;";
                cmd.Parameters.AddWithValue("@charId", chr.CharacterId);
                cmd.ExecuteNonQuery();
            }
            }

            //            cmd = CreateQuery();
            //            cmd.CommandText = @"INSERT INTO classstats(accId, objType, bestLv, bestFame) 
            //VALUES(@accId, @objType, @bestLv, @bestFame) 
            //ON DUPLICATE KEY UPDATE 
            //bestLv = GREATEST(bestLv, @bestLv), 
            //bestFame = GREATEST(bestFame, @bestFame);";
            //            cmd.Parameters.AddWithValue("@accId", acc.AccountId);
            //            cmd.Parameters.AddWithValue("@objType", chr.ObjectType);
            //            cmd.Parameters.AddWithValue("@bestLv", chr.Level);
            //            cmd.Parameters.AddWithValue("@bestFame", finalFame);
            //            cmd.ExecuteNonQuery();

            if (acc.Guild.Id != 0)
            {
                cmd = CreateQuery();
                cmd.CommandText = @"UPDATE guilds SET
guildFame=guildFame+@amount,
totalGuildFame=totalGuildFame+@amount
WHERE name=@name;";
                cmd.Parameters.AddWithValue("@amount", finalFame);
                cmd.Parameters.AddWithValue("@name", acc.Guild.Name);
                cmd.ExecuteNonQuery();

                cmd = CreateQuery();
                cmd.CommandText = @"UPDATE accounts SET
guildFame=guildFame+@amount
WHERE id=@id;";
                cmd.Parameters.AddWithValue("@amount", finalFame);
                cmd.Parameters.AddWithValue("@id", acc.AccountId);
                cmd.ExecuteNonQuery();
            }
    //        if(acc.Rank == 1)
    //        {
    //            cmd = CreateQuery();
    //            cmd.CommandText =
    //                @"INSERT INTO death(accId, chrId, name, charType, tex1, tex2, skin, items, fame, exp, fameStats, totalFame, firstBorn, killer) 
    //VALUES(@accId, @chrId, @name, @objType, @tex1, @tex2, @skin, @items, @fame, @exp, @fameStats, @totalFame, @firstBorn, @killer);";
    //            cmd.Parameters.AddWithValue("@accId", acc.AccountId);
    //            cmd.Parameters.AddWithValue("@chrId", chr.CharacterId);
    //            cmd.Parameters.AddWithValue("@name", acc.Name);
    //            cmd.Parameters.AddWithValue("@objType", chr.ObjectType);
    //            cmd.Parameters.AddWithValue("@tex1", chr.Tex1);
    //            cmd.Parameters.AddWithValue("@tex2", chr.Tex2);
    //            cmd.Parameters.AddWithValue("@skin", chr.Skin);
    //            cmd.Parameters.AddWithValue("@items", chr._Equipment);
    //            cmd.Parameters.AddWithValue("@fame", chr.CurrentFame);
    //            cmd.Parameters.AddWithValue("@exp", chr.Exp);
    //            cmd.Parameters.AddWithValue("@fameStats", chr.PCStats);
    //            cmd.Parameters.AddWithValue("@totalFame", finalFame);
    //            cmd.Parameters.AddWithValue("@firstBorn", firstBorn);
    //            cmd.Parameters.AddWithValue("@killer", killer);
    //            cmd.ExecuteNonQuery();
    //        }
        }
