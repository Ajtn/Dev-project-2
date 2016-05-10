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

            if (!section.SectionInformation.IsProtected) //Ensure connection string is always encrypted
            {
                // Encrypt the appropriate section in app.config.
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                config.Save();
            }

            myConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["databaseConnectionString"].ToString()); //Retrieve the encrypted string
        }

        public bool openDBConnection() //Opens a connection on the MySqlConnection object
        {
            try { myConnection.Open(); }
           
            catch (MySqlException) { return false; }
            
            return true;
        }

        //Find the primary key of a row, given a specified value.
        //columnToSearch represents the column you wish to search through, for example Date or Name
        //valueToFind represents the value you want to find in the column you want to search, for example 1995-12-12 or Lionel Bersee
        //primaryKey represents the name of the primary key column, for example ItemID
        //tableName represents the name of the table you wish to search, for example Item
        public virtual string findPrimaryKey(string columnToSearch, string valueToFind, string primaryKey, string tableName)
        {
            string myQuery = "Select " + primaryKey + " FROM " + tableName;

            myQuery += " WHERE " + columnToSearch + "=" + "'" +valueToFind+"'"; //Build initial query

            MySqlDataReader myReader;

            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection); //Create our command

            myReader = myCommand.ExecuteReader(); //execute our command

            string data = "";
            myReader.Read();
            data = myReader.GetString(primaryKey); //returns the primaryKey of the returned row as a string
            myReader.Close();

            return data;
        }

        //Find the primary key of a row, given two values that must match in the retrieved record.
        //columnToSearch represents the column you wish to search through, for example Date or Name
        //valueToFind represents the value you want to find in the column you want to search, for example 1995-12-12 or Lionel Bersee
        //primaryKey represents the name of the primary key column, for example ItemID
        //tableName represents the name of the table you wish to search, for example Item
        public string findPrimaryKey(string columnToSearch1, string columnToSearch2, string valueToFind1, string valueToFind2, string primaryKey, string tableName)
        {
            string myQuery = "Select " + primaryKey + " FROM " + tableName;

            myQuery += " WHERE " + columnToSearch1 + "=" + "'" + valueToFind1 + "'" + " AND " + columnToSearch2 + "=" + "'" + valueToFind2 + "'";

            MySqlDataReader myReader;

            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);

            myReader = myCommand.ExecuteReader();

            string data = "";
            myReader.Read();
            data = myReader.GetString(primaryKey);
            myReader.Close();

            return data;
        }


        //This method selects all records in a particular table and returns the appropriate 2d array.
        //dbHeaders represents the columns to be read from the table, values here must match the column names exactly.
        public string[,] runDatabaseQuery(string[] dbHeaders, string tableName) //Select all from a table
        {
            MySqlDataReader myReader;
            string[,] data;

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
        //This method selects from two tables, linking them via an inner join.
        //tableOne and tableTwo represent the names of the tables that we wish to join.
        //columnsTableOne and columnsTableTwo represent the columns from each table we wish to display.
        //columnToJoin represents the name of the column we wish to join, this column must be present in both tables.
        public virtual string[,] runDatabaseQuery( string tableOne, string tableTwo, string[] ColumnsTableOne, string[] ColumnsTableTwo, string ColumnToJoin) 
        {
            MySqlDataReader myReader;
            string[,] data;

            //When inserting data via inner joins, table aliases must be set
            //The following for loops insert t1. and t2. to the appropriate column names.
            for (int i = 0; i < ColumnsTableOne.Length; i++)
            {
                 ColumnsTableOne[i] = ColumnsTableOne[i].Insert(0, "t1."); //Insert t1. at the 0 index in our string
            }
            
            for (int i = 0; i < ColumnsTableTwo.Length; i++)
            {
                ColumnsTableTwo[i] = ColumnsTableTwo[i].Insert(0, "t2.");
            }

            //appends t1 and t2 to the tableOne and tableTwo, this must be done to properly assign the alias to the table
            tableOne += " t1"; 
            tableTwo += " t2"; //tableTwo: t2.Item t2


            //The following code builds the appropriate query to select from our specified columns
            string myQuery = "SELECT "; 

            for (int i = 0; i < ColumnsTableOne.Length; i++)
            {
                myQuery += ColumnsTableOne[i];
                myQuery += ", ";
            }

            for (int i = 0; i < ColumnsTableTwo.Length; i++)
            {
                myQuery += ColumnsTableTwo[i];

                if (i != (ColumnsTableTwo.Length - 1)) //check if we need to insert a comma, if we don't insert a space
                {
                    myQuery += ", ";
                }
                else myQuery += " ";
            }


            myQuery += "FROM " + tableOne + " INNER JOIN " + tableTwo + " ON " + "t1." + ColumnToJoin + "=t2." + ColumnToJoin; //myQuery represents the query to be sent to the database
            string myQuery2 = "SELECT COUNT(*) " + "FROM " + tableOne + " INNER JOIN " + tableTwo + " ON " + "t1." + ColumnToJoin + "=t2." + ColumnToJoin; //the query string responsible for requesting the number of rows


            //Create and initialise command objects
            MySqlCommand myCommand2 = new MySqlCommand(myQuery2, myConnection);
            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);

            int rows = Convert.ToInt32(myCommand2.ExecuteScalar()); //Runs the second command, returning a long converted to an int
            myReader = myCommand.ExecuteReader(); //initialises the reader object

            //The following for loops remove the aliases prepended to the column names. This is so we can use the column names as arguments to myReader.GetString().
            for (int i = 0; i < ColumnsTableOne.Length; i++)
            {
                ColumnsTableOne[i] = ColumnsTableOne[i].Remove(0, 3); //Remove 3 characters beginning at the 0 index of our string
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
        
        //This method deletes a database row, given a particular ID.
        //This method is incompatible with the new database and must be reworked
        public void deleteDatabaseRow(int id, string tableName )
        {
            string myQuery = "DELETE FROM " + tableName + " WHERE " + GameMain.pTable.pHeader[0].Replace(" ", string.Empty) + " =" + id;
            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);
            //myCommand.ExecuteNonQuery();
        }

        //This method adds a row to the database.
        //myArguments represents the values you wish to insert into the table
        //myColumns represents the columns you wish to insert values into
        //tableName represents the name of the table
        //IMPORTANT NOTE: the order of columns and arguments in each array need to match
        //                for instance, if you specify myArguments[0] = "Lionel" then myColumns[0] = "Name"
        public void addDatabaseRow(string[] myArguments, string[] myColumns, string tableName)
        {
            string myQuery = "INSERT INTO `" + tableName + "`(";


            //The following for loops build the query string
            for (int j = 0; j < myArguments.Length; j++ )
            {
                if (myArguments[j] != null)
                {
                    myQuery += "`" + myColumns[j] + "`";

                    if (j < myArguments.Length - 1) myQuery += ", ";
                }
            }

            myQuery += ") VALUES (";

            for (int i = 0; i < myArguments.Length; i++ )
            {
                if (myArguments[i] != null)
                {
                    myQuery += "'" + myArguments[i] + "'";
                }
                if (i < myArguments.Length - 1) myQuery += ",";

                
            }
            myQuery += ")";
            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);
            myCommand.ExecuteNonQuery(); //INSERT INTO `Sale`(`NumberSold`, `SaleID`) VALUES ('22','99')
        }

        //This method checks whether a record exists in a table, returning true if a record exists.
        //tableName represents the name of the table you wish to query
        //columnOne represents the column you wish to search
        //valueOne represents the value you wish to find in the column you have specified
        public virtual bool checkRecordExists(string tableName, string columnOne, string valueOne)
        {
            bool result = false;
            int rows = 0;
            string myQuery = "";


            myQuery += "SELECT COUNT(*) FROM " + tableName + " WHERE " + columnOne + "=" + "'" + valueOne + "'";
            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);


            rows = Convert.ToInt32(myCommand.ExecuteScalar()); //Store the amount of rows returned from running the query

            if (rows == 0) return result; //If we recieved more than 0 rows, return true, otherwise return false.
            else return true;
        }

        //This method checks if a record exists, given two columns to search.
        //This method is required when checking tables that may have columns which are not unique.
        //For instance, the StockOrder table contains duplicate dates, and duplicate employee names.
        //Checking if a particular record exists in the StockOrder tables requires us to specify both a date and
        //an employee name, this method provides functionality to do that.
        //tableName represents the name of the table to query
        //columnOne and columnTwo represent the columns to search
        //valueOne and valueTwo represent the values to search for in the specified columns
        public bool checkRecordExists(string tableName, string columnOne, string columnTwo, string valueOne, string valueTwo)
        {
            bool result = false;
            int rows = 0;
            string myQuery = "";

            //Build the query string.
            myQuery += "SELECT COUNT(*) FROM " + tableName + " WHERE " + columnOne + "=" + "'" + valueOne + "'" + " AND " + columnTwo + "=" + "'" + valueTwo + "'";
            MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);


            rows = Convert.ToInt32(myCommand.ExecuteScalar()); //Store the amount of rows returned from running the query

            if (rows == 0) return result;  //If we recieved more than 0 rows, return true, otherwise return false.
            else return true;
        }

        public void ExportAsCSV(string tableOne, string tableTwo, string tableThree,
                                                  string[] ColumnsTableOne, string[] ColumnsTableTwo, string[] ColumnsTableThree,
                                                  string ColumnToJoinA, string ColumnToJoinB, string DateStart, string DateEnd)
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

            myQuery += "FROM " + tableOne + " INNER JOIN " + tableTwo + " ON " + "t1." + ColumnToJoinA + "= t2." + ColumnToJoinA + " INNER JOIN " + tableThree + " ON " + "t1." + ColumnToJoinB + "= t3." + ColumnToJoinB + " WHERE " + ColumnsTableTwo[0] + " BETWEEN " + "'" + DateStart + "'" + " AND" + "'" + DateEnd + "'";
            string myQuery2 = "SELECT COUNT(*) FROM " + tableOne + " INNER JOIN " + tableTwo + " ON " + "t1." + ColumnToJoinA + "= t2." + ColumnToJoinA + " INNER JOIN " + tableThree + " ON " + "t1." + ColumnToJoinB + "= t3." + ColumnToJoinB + " WHERE " + ColumnsTableTwo[0] + " BETWEEN " + "'" + DateStart + "'" + " AND" + "'" + DateEnd + "'"; //the query string responsible for requesting the number of rows

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

            // WRITE TO TEXT FILE WITH CSV
            string[] lTemp = new string[data.GetLength(0)];

            for (int j = 0; j < data.GetLength(0); j++)
            {
                for (int i = 0; i < data.GetLength(1); i++)
                {
                    if (i == 0)
                        lTemp[j] += (data[j, i]);
                    else
                        lTemp[j] += (", " + data[j, i]);
                }
            }
            System.IO.File.AppendAllLines("csv.txt", lTemp);
        }


    }
}