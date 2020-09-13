using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp_Contacts_App
{
    class USER
    {

        MY_DB db = new MY_DB();

        public DataTable getUserById(Int32 userid)
        {
            
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            DataTable table = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `user` WHERE `id` = @uid", db.getConnection);
            command.Parameters.Add("@uid", MySqlDbType.Int32).Value = userid;

            adapter.SelectCommand = command;

            adapter.Fill(table);

            return table;
        }


        // function to insert a new user
        public bool insertUser(string fname, string lname, string username, string password, MemoryStream picture)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `user`(`fname`, `lname`, `username`, `pass`, `pic`) VALUES (@fn, @ln, @un, @pass, @pic)", db.getConnection);

            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@un", MySqlDbType.VarChar).Value = username;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
            command.Parameters.Add("@pic", MySqlDbType.Blob).Value = picture.ToArray();

            db.openConnection();

            if ( command.ExecuteNonQuery() == 1 )
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }

        }


        // function to check if the username already exists
        public bool usernameExist(string username, string operation, int userid = 0)
        {
            string query = "";

            if (operation == "register")
            {
                // if a new user want to register we will check if the username already exists
                query = "SELECT * FROM `user` WHERE username = @un";
            }
            else if(operation == "edit")
            {
                // if an existing student want to edit his information 
                // we will check if he enter an existing username ( not including his own username )
                query = "SELECT * FROM `user` WHERE username = @un AND `id` <> @uid";
            }

            MySqlCommand command = new MySqlCommand(query, db.getConnection);

            command.Parameters.Add("@un", MySqlDbType.VarChar).Value = username;
            command.Parameters.Add("@uid", MySqlDbType.Int32).Value = userid;

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        // function to update the logged user data
        public bool updateUser(int userid, string fname, string lname, string username, string password, MemoryStream picture)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `user` SET `fname`= @fn,`lname`= @ln,`username`= @un,`pass`= @pass,`pic`= @pic WHERE `id` = @uid", db.getConnection);

            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@un", MySqlDbType.VarChar).Value = username;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
            command.Parameters.Add("@pic", MySqlDbType.Blob).Value = picture.ToArray();
            command.Parameters.Add("@uid", MySqlDbType.Int32).Value = userid;

            db.openConnection();

            if ( command.ExecuteNonQuery() == 1 )
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }

        }


    }
}
