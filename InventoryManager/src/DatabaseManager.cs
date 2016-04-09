using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace MyGame
{
    public class DatabaseManager
    {
        SqlConnection myConnection;

        public DatabaseManager(string dbServer, string dbName, string dbUser, string dbPW)
        {
            myConnection = new SqlConnection(
                "Data Source=" + dbServer + ";" +
                "Database=" + dbName + ";" +
                "Uid=" + dbUser + ";" +
                "Pwd=" + dbPW + ";");
        }

        public bool OpenDBConnection()
        {
            myConnection.Open();

            return true;
        }
    }
}