using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Csharp_Cumulative1_n01651646.Models
{
    public class SchoolDbContext
    {

        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "schoodb"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        //ConnectionString is a series of credentials used to connect to the database.
        protected static string ConnectionString
        {
            get
            {
                // Convert Zero Datetime is a setting that will interpret a 0000-00-00 as null
                // This makes it easier for C# to convert to a proper DateTime type
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
            }
        }
        //This is the method we actually use to get the database!
        /// <summary>
        /// Returns a connection to the schoodb database.
        /// </summary>
        /// <example>
        /// private SchoolDbDbContext schoodb = new DbContext();
        /// MySqlConnection Conn = schoodb.AccessDatabase();
        /// </example>
        /// <returns>A MySqlConn</returns>
        public MySqlConnection AccessDatabase()
        {
            //We are instantiating the MySqlConnection Class to create an object
            //the object is a connection to the schoodb database on port 3306 of localhost
            return new MySqlConnection(ConnectionString);
        }
    }
    }