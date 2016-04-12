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

        public string[,] runCurrentStockQuery()
        {
            MySqlDataReader myReader;
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
            myReader.Close();
            return data;
        }

        public string[,] runSaleQuery()
        {
            MySqlDataReader myReader;
            string[] header = new string[] { "StockID", "NumberSold", "SaleID" };
            string[,] data;
            List<String> result = new List<String>();

            string myQuery = "SELECT * FROM Sale";

            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);
            myReader = myCommand.ExecuteReader();

            data = new string[5, header.Length];

            int Y = 0;
            int X = 0;

            while (myReader.Read())
            {
                data[Y, X] = myReader.GetString("StockID");
                X++;
                data[Y, X] = myReader.GetString("NumberSold");
                X++;
                data[Y, X] = myReader.GetString("SaleID");
                X = 0;
                Y++;
            }
            myReader.Close();
            return data;
        }

        public string[,] runOrdersQuery()
        {
            MySqlDataReader myReader;
            string[] header = new string[] { "OrderID", "DateOrdered", "DateExpected", "Processed", "ValueOfOrder" };
            string[,] data;
            List<String> result = new List<String>();

            string myQuery = "SELECT * FROM Orders";

            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);
            myReader = myCommand.ExecuteReader();

            data = new string[10, header.Length];

            int Y = 0;
            int X = 0;

            while (myReader.Read())
            {
                data[Y, X] = myReader.GetString("OrderID");
                X++;
                data[Y, X] = myReader.GetString("DateOrdered");
                X++;
                data[Y, X] = myReader.GetString("DateExpected");
                X++;
                data[Y, X] = myReader.GetString("Processed");
                X++;
                data[Y, X] = myReader.GetString("ValueOfOrder");
                X = 0;
                Y++;
            }
            myReader.Close();
            return data;
        }


        public string[,] runItemsQuery()
        {
            MySqlDataReader myReader;
            string[] header = new string[] { "OrderID", "DateOrdered", "DateExpected", "Processed", "ValueOfOrder" };
            string[,] data;
            List<String> result = new List<String>();

            string myQuery = "SELECT * FROM StockItem";

            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);
            myReader = myCommand.ExecuteReader();

            data = new string[5, header.Length];

            int Y = 0;
            int X = 0;

            while (myReader.Read())
            {
                data[Y, X] = myReader.GetString("StockID");
                X++;
                data[Y, X] = myReader.GetString("ProductName");
                X++;
                data[Y, X] = myReader.GetString("SalePrice");
                X++;
                data[Y, X] = myReader.GetString("OrderPrice");
                X++;
                data[Y, X] = myReader.GetString("AverageSold");
                X = 0;
                Y++;
            }
            myReader.Close();
            return data;
        }
    }
}