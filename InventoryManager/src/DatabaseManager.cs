using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace MyGame
{
    public class DatabaseManager
    {
        MySqlConnection myConnection;

        //The default constructor initializes a new MySqlConnection object with a connection string
        public DatabaseManager() 
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration("MyGame.exe");

            ConnectionStringsSection section = config.GetSection("connectionStrings") as ConnectionStringsSection;

            if (!section.SectionInformation.IsProtected)
            {
                // Encrypt the section.
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                config.Save();
            }

            myConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["databaseConnectionString"].ToString());
        }

        public bool openDBConnection() //Opens a connection on the MySqlConnection object
        {
            try { myConnection.Open(); }
           
            catch (MySqlException) { return false; }
            
            return true;
        }

        public string[,] runDatabaseQuery(string[] dbHeaders, string tableName) //Select all from a table
        {
            MySqlDataReader myReader;
            string[,] data;
            List<String> result = new List<String>();

            string myQuery = "SELECT * FROM " + tableName; //sets the main query string
            string myQuery2 = "SELECT COUNT(*) FROM " + tableName; //the query string responsible for requesting the number of rows

            //Create and initialise command objects
            MySqlCommand myCommand2 = new MySqlCommand(myQuery2, myConnection);
            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);

            int rows = Convert.ToInt32(myCommand2.ExecuteScalar()); //Runs the second command, returning a long converted to an int
            myReader = myCommand.ExecuteReader(); //initialises the reader object

            data = new string[rows, dbHeaders.Length]; //Set a new 2d array with the amount of rows and amount of columns

            //Loop through each element in our data array, reading in the appropriate data from the database
            for (int Y = 0; Y < rows; Y++)
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
        //Select from two tables using an inner join
        public virtual string[,] runDatabaseQuery( string tableOne, string tableTwo, string[] ColumnsTableOne, string[] ColumnsTableTwo, string ColumnToJoin) 
        {
            MySqlDataReader myReader;
            string[,] data;

            for (int i = 0; i < ColumnsTableOne.Length; i++)
            {
                 ColumnsTableOne[i] = ColumnsTableOne[i].Insert(0, "t1.");
            }
            
            for (int i = 0; i < ColumnsTableTwo.Length; i++)
            {
                ColumnsTableTwo[i] = ColumnsTableTwo[i].Insert(0, "t2.");
            }

            tableOne += " t1";
            tableTwo += " t2";



            string myQuery = "SELECT ";

            for (int i = 0; i < ColumnsTableOne.Length; i++)
            {
                myQuery += ColumnsTableOne[i];

                
             
                    myQuery += ", ";
         
            }

            for (int i = 0; i < ColumnsTableTwo.Length; i++)
            {
                myQuery += ColumnsTableTwo[i];

                if (i != (ColumnsTableTwo.Length - 1))
                {
                    myQuery += ", ";
                }
                else myQuery += " ";
            }


            myQuery += "FROM " + tableOne + " INNER JOIN " + tableTwo + " ON " + "t1." + ColumnToJoin + "=t2." + ColumnToJoin;
            string myQuery2 = "SELECT COUNT(*) " + "FROM " + tableOne + " INNER JOIN " + tableTwo + " ON " + "t1." + ColumnToJoin + "=t2." + ColumnToJoin; //the query string responsible for requesting the number of rows


            //Create and initialise command objects
            MySqlCommand myCommand2 = new MySqlCommand(myQuery2, myConnection);
            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);

            int rows = Convert.ToInt32(myCommand2.ExecuteScalar()); //Runs the second command, returning a long converted to an int
            myReader = myCommand.ExecuteReader(); //initialises the reader object

            for (int i = 0; i < ColumnsTableOne.Length; i++)
            {
                ColumnsTableOne[i] = ColumnsTableOne[i].Remove(0, 3);
            }

            for (int i = 0; i < ColumnsTableTwo.Length; i++)
            {
                ColumnsTableTwo[i] = ColumnsTableTwo[i].Remove(0, 3);
            }

            data = new string[rows, ColumnsTableOne.Length + ColumnsTableTwo.Length]; //Set a new 2d array with the amount of rows and amount of columns

            int temp = 0;
            //Loop through each element in our data array, reading in the appropriate data from the database
            for (int Y = 0; Y < rows; Y++)
            {
                myReader.Read(); //called each iteration of the loop in order to read in new rows
                for (int i = 0; i < ColumnsTableOne.Length; i++)
                {
                    data[Y, i] = myReader.GetString(ColumnsTableOne[i]);
                    temp = i;

                }
                temp++;
                for (int i = 0; i < ColumnsTableTwo.Length; i++)
                {
                    data[Y, temp] = myReader.GetString(ColumnsTableTwo[i]);
                    temp++;
                }
            }

            // warn of low stock (THRESHOLD IS 10)
            string lTemp = "";
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (Int32.Parse(data[i, 0]) < 10)
                {
                    if (lTemp == "")
                        lTemp += data[i, 1];
                    else
                        lTemp += '\n' + data[i, 1];
                }
            }
            if (lTemp != "")
                MessageBox.Show("The current items are in low stock" + '\n' + lTemp, "LOW STOCK");

            myReader.Close(); //close the data reader
            return data;
        }

        //Select from three tables using two inner joins.
        public virtual string[,] runDatabaseQuery(string tableOne, string tableTwo, string tableThree, 
                                                  string[] ColumnsTableOne, string[] ColumnsTableTwo, string[] ColumnsTableThree, 
                                                  string ColumnToJoinA, string ColumnToJoinB)
        {
            MySqlDataReader myReader;
            string[,] data;

            for (int i = 0; i < ColumnsTableOne.Length; i++)
            {
                ColumnsTableOne[i] = ColumnsTableOne[i].Insert(0, "t1.");
            }

            for (int i = 0; i < ColumnsTableTwo.Length; i++)
            {
                ColumnsTableTwo[i] = ColumnsTableTwo[i].Insert(0, "t2.");
            }

            for (int i = 0; i < ColumnsTableThree.Length; i++)
            {
                ColumnsTableThree[i] = ColumnsTableThree[i].Insert(0, "t3.");
            }

            tableOne += " t1";
            tableTwo += " t2";
            tableThree += " t3";



            string myQuery = "SELECT ";

            for (int i = 0; i < ColumnsTableOne.Length; i++)
            {
                myQuery += ColumnsTableOne[i];



                myQuery += ", ";

            }

            for (int i = 0; i < ColumnsTableTwo.Length; i++)
            {
                myQuery += ColumnsTableTwo[i];



                myQuery += ", ";

            }

            for (int i = 0; i < ColumnsTableThree.Length; i++)
            {
                myQuery += ColumnsTableThree[i];

                if (i != (ColumnsTableThree.Length - 1))
                {
                    myQuery += ", ";
                }
                else myQuery += " ";
            }


            myQuery += "FROM " + tableOne + " INNER JOIN " + tableTwo + " ON " + "t1." + ColumnToJoinA + "= t2." + ColumnToJoinA + " INNER JOIN " + tableThree + " ON " + "t1." + ColumnToJoinB + "= t3." + ColumnToJoinB;
            string myQuery2 = "SELECT COUNT(*) FROM " + tableOne + " INNER JOIN " + tableTwo + " ON " + "t1." + ColumnToJoinA + "= t2." + ColumnToJoinA + " INNER JOIN " + tableThree + " ON " + "t1." + ColumnToJoinB + "= t3." + ColumnToJoinB; //the query string responsible for requesting the number of rows


            //Create and initialise command objects
            MySqlCommand myCommand2 = new MySqlCommand(myQuery2, myConnection);
            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);

            int rows = Convert.ToInt32(myCommand2.ExecuteScalar()); //Runs the second command, returning a long converted to an int
            myReader = myCommand.ExecuteReader(); //initialises the reader object

            for (int i = 0; i < ColumnsTableOne.Length; i++)
            {
                ColumnsTableOne[i] = ColumnsTableOne[i].Remove(0, 3);
            }

            for (int i = 0; i < ColumnsTableTwo.Length; i++)
            {
                ColumnsTableTwo[i] = ColumnsTableTwo[i].Remove(0, 3);
            }

            for (int i = 0; i < ColumnsTableThree.Length; i++)
            {
                ColumnsTableThree[i] = ColumnsTableThree[i].Remove(0, 3);
            }

            data = new string[rows, ColumnsTableOne.Length + ColumnsTableTwo.Length + ColumnsTableThree.Length]; //Set a new 2d array with the amount of rows and amount of columns

            int temp = 0;
            //Loop through each element in our data array, reading in the appropriate data from the database
            for (int Y = 0; Y < rows; Y++)
            {
                myReader.Read(); //called each iteration of the loop in order to read in new rows
                for (int i = 0; i < ColumnsTableOne.Length; i++)
                {
                    data[Y, i] = myReader.GetString(ColumnsTableOne[i]);
                    temp = i;

                }
                temp++;
                for (int i = 0; i < ColumnsTableTwo.Length; i++)
                {
                    data[Y, temp] = myReader.GetString(ColumnsTableTwo[i]);
                    temp++;
                }
                for (int i = 0; i < ColumnsTableThree.Length; i++)
                {
                    data[Y, temp] = myReader.GetString(ColumnsTableThree[i]);
                    temp++;
                }

            }

            myReader.Close(); //close the data reader
            return data;
        }
        
        public void deleteDatabaseRow(int id, string tableName )
        {
            string myQuery = "DELETE FROM " + tableName + " WHERE " + GameMain.pTable.pHeader[0].Replace(" ", string.Empty) + " =" + id;
            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);
            myCommand.ExecuteNonQuery();
        }

        public void addDatabaseRow(string[] myArguments, string tableName)
        {
            string myQuery = "INSERT INTO `" + tableName + "`(";

            for (int j = 0; j < GameMain.pTable.pHeader.Length; j++ )
            {
                if (myArguments[j] != null)
                {
                    myQuery += "`" + GameMain.pTable.pHeader[j].Replace(" ", string.Empty) + "`";

                    if (j < GameMain.pTable.pHeader.Length - 1) myQuery += ", ";
                }
            }

            myQuery += ") VALUES (";

            for (int i = 0; i < GameMain.pTable.pHeader.Length; i++ )
            {
                if (myArguments[i] != null)
                {
                    myQuery += "'" + myArguments[i] + "'";
                }
                if (i < GameMain.pTable.pHeader.Length - 1 && myArguments[i] != null) myQuery += ",";

                
            }
            myQuery += ")";
            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);
            myCommand.ExecuteNonQuery(); //INSERT INTO `Sale`(`NumberSold`, `SaleID`) VALUES ('22','99')
        }

        public virtual void addDatabaseRow(string[] myArguments, string tableName1, string tableName2)
        {
 
        }
        
    }
}