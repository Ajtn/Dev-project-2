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

        public string[,] runQuery()
        {
            string[] header = new string[] { "StockID", "ProductName", "NumberInStock"};
            string[,] data;
            List<String> result = new List<String>();

            string myQuery = "SELECT * FROM CurrentStock";

            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);
            myReader = myCommand.ExecuteReader();

            data = new string[2, header.Length];

            int Y = 0;
            int X = 0;

            while (myReader.Read())
            {
                data[Y, X] = myReader.GetString("StockID");
                X++;
                data[Y, X] = myReader.GetString("ProductName");
                X++;
                data[Y, X] = myReader.GetString("NumberInStock");
                X = 0;
                Y++;
            }
            return data;
        }

        public string runQuery(string table)
        {
            return null;
        }
    }
}