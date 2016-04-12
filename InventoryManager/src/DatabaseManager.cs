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

        public string[,] runDatabaseQuery(string[] dbHeaders, string tableName)
        {
            MySqlDataReader myReader;
            string[,] data;
            List<String> result = new List<String>();

            string myQuery = "SELECT * FROM " + tableName;
            string myQuery2 = "SELECT COUNT(*) FROM " +tableName;

            MySqlCommand myCommand2 = new MySqlCommand(myQuery2, myConnection); 

            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);
            int rows = Convert.ToInt32(myCommand2.ExecuteScalar());
            myReader = myCommand.ExecuteReader();

            data = new string[rows, dbHeaders.Length];

                for (int Y = 0; Y < rows; Y++ )
                {
                    myReader.Read();
                    for (int i = 0; i < dbHeaders.Length; i++)
                    {
                        data[Y, i] = myReader.GetString(dbHeaders[i]);
                    }
                }

            myReader.Close();
            return data;
        }
    }
}