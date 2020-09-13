using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Csharp_Contacts_App
{
    class GROUP
    {

        MY_DB mydb = new MY_DB();


        // function to insert a new group for a specific user
        public bool insertGroup(string gname, int userid)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `mygroups`(`name`, `userid`) VALUES (@gn, @uid)", mydb.getConnection);

            command.Parameters.Add("@gn", MySqlDbType.VarChar).Value = gname;
            command.Parameters.Add("@uid", MySqlDbType.Int32).Value = userid;

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



        // function to update the selected group
        public bool updateGroup(int gid, string gname)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `mygroups` SET `name`= @name WHERE `id` = @id", mydb.getConnection);

            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = gname;
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = gid;

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


        // function to delete the selected group
        public bool deleteGroup(int groupid)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `mygroups` WHERE `id` = @id", mydb.getConnection);

            command.Parameters.Add("@id", MySqlDbType.Int32).Value = groupid;

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


        // function to get contact groups for a specific user
        public DataTable getGroups( int userid )
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM mygroups WHERE userid = @uid", mydb.getConnection);
           
            command.Parameters.Add("@uid", MySqlDbType.Int32).Value = userid;
            command.Connection = mydb.getConnection;
            
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            
            DataTable table = new DataTable();
            
            adapter.Fill(table);
            
            return table;
        }



        // function to check if the group name already exists ( for the logged user )
        public bool groupExist(string name, string operation, int userid = 0, int groupid = 0)
        {
            string query = "";

            MySqlCommand command = new MySqlCommand();

            if (operation == "add") // when inserting a new group
            {
                // if the new group name already exists
                query = "SELECT * FROM `mygroups` WHERE name = @name AND `userid` = @uid";
                
                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                command.Parameters.Add("@uid", MySqlDbType.Int32).Value = userid;
            }
            else if (operation == "edit") // when editing a group name
            {
                // we will check if the enter an existing group name
                query = "SELECT * FROM `mygroups` WHERE name = @name AND `userid` = @uid AND `id` <> @gid";

                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                command.Parameters.Add("@uid", MySqlDbType.Int32).Value = userid;
                command.Parameters.Add("@gid", MySqlDbType.Int32).Value = groupid;
            }

            command.Connection = mydb.getConnection;
            command.CommandText = query;
            
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
