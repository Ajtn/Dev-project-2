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

        public bool OpenDBConnection() //Opens a connection on the MySqlConnection object
        {
            try { myConnection.Open(); }
           
            catch (MySqlException) { return false; }
            
            return true;
        }

        public string[,] runDatabaseQuery(string[] dbHeaders, string tableName) //Method for handling database ueries
        {
            MySqlDataReader myReader; 
            string[,] data;
            List<String> result = new List<String>();

            string myQuery = "SELECT * FROM " + tableName; //sets the main query string
            string myQuery2 = "SELECT COUNT(*) FROM " +tableName; //the query string responsible for requesting the number of rows

            //Create and initialise command objects
            MySqlCommand myCommand2 = new MySqlCommand(myQuery2, myConnection);
            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);

            int rows = Convert.ToInt32(myCommand2.ExecuteScalar()); //Runs the second command, returning a long converted to an int
            myReader = myCommand.ExecuteReader(); //initialises the reader object

            data = new string[rows, dbHeaders.Length]; //Set a new 2d array with the amount of rows and amount of columns

                //Loop through each element in our data array, reading in the appropriate data from the database
                for (int Y = 0; Y < rows; Y++ )
                {
                    myReader.Read(); //called each iteration of the loop in order to read in new rows
                    for (int i = 0; i < dbHeaders.Length; i++)
                    {
                        data[Y, i] = myReader.GetString(dbHeaders[i]);
                    }
                }

            myReader.Close(); //close the data reader
            return data;
        }
    }
}