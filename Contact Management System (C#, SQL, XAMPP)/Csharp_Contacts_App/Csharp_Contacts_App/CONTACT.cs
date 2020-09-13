using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;

namespace Csharp_Contacts_App
{
    class CONTACT
    {

        MY_DB mydb = new MY_DB();


        // function to insert a new contact
        public bool insertContact(string fname, string lname, string phone, string address, string email, int userid, int groupid, MemoryStream picture)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `mycontact`(`fname`, `lname`, `group_id`, `phone`, `email`, `address`, `pic`, `userid`) VALUES (@fn, @ln, @grp, @phn, @mail, @adrs, @pic, @uid)", mydb.getConnection);

            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@grp", MySqlDbType.Int32).Value = groupid;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = email;
            command.Parameters.Add("@adrs", MySqlDbType.VarChar).Value = address;
            command.Parameters.Add("@uid", MySqlDbType.Int32).Value = userid;
            command.Parameters.Add("@pic", MySqlDbType.Blob).Value = picture.ToArray();

            mydb.openConnection();

            if ((command.ExecuteNonQuery() == 1))
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }

        }



        // function to update the selected contact
        public bool updateContact(int contactid, string fname, string lname, string phone, string address, string email, int groupid, MemoryStream picture)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `mycontact` SET `fname`= @fn,`lname`= @ln,`group_id`= @gid,`phone`= @phn,`email`= @mail,`address`= @adrs,`pic`= @pic WHERE `id` = @id", mydb.getConnection);

            command.Parameters.Add("@id", MySqlDbType.Int32).Value = contactid;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@gid", MySqlDbType.Int32).Value = groupid;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = email;
            command.Parameters.Add("@adrs", MySqlDbType.VarChar).Value = address;
            command.Parameters.Add("@pic", MySqlDbType.Blob).Value = picture.ToArray();

            mydb.openConnection();

            if ( command.ExecuteNonQuery() == 1 )
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }

        }


        // function to delete the selected contact
        public bool deleteContact(int contactid)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `mycontact` WHERE `id` = @id", mydb.getConnection);

            command.Parameters.Add("@id", MySqlDbType.Int32).Value = contactid;
            
            mydb.openConnection();

            if ((command.ExecuteNonQuery() == 1))
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }

        }


        // function to return the contacts list depending on the given command
        public DataTable SelectContactList(MySqlCommand command)
        {
            command.Connection = mydb.getConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }



        public DataTable GetContactById(Int32 contactId)
        {
            MySqlCommand command = new MySqlCommand("SELECT `id`, `fname`, `lname`, `group_id`, `phone`, `email`, `address`, `pic`, `userid` FROM `mycontact` WHERE `id` = @id", mydb.getConnection);
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = contactId;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }



    }
}
