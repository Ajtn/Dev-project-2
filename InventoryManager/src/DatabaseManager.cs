using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace MyGame
{
    public class DatabaseManager
    {
        MySqlConnection myConnection;
        MySqlDataReader myReader;

        //The default constructor initializes a new MySqlConnection object with a connection string
        public DatabaseManager(string dbServer, string dbName, string dbUser, string dbPW) 
        {
            myConnection = new MySqlConnection(
                "Server=" + dbServer + ";" +
                "Database=" + dbName + ";" +
                "Uid=" + dbUser + ";" +
                "Password=" + dbPW + ";");
        }

        public bool OpenDBConnection()
        {
            try { myConnection.Open(); }
           
            catch (MySqlException) { return false; }
            
            return true;
        }

        public string runQuery(string table, string query)
        {
            string result = "";
            string myQuery = "SELECT " + query + " FROM " + "/'/" + table + "/'/";

            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);
            myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                result += myReader.GetString("Stock_ID");
            }

            return result;
       
        }

        public string runQuery(string table)
        {
            return null;
        }
    }
}