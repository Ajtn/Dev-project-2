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
    }
}