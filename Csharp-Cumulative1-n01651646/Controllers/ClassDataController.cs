using Csharp_Cumulative1_n01651646.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Csharp_Cumulative1_n01651646.Controllers
{
    public class ClassDataController : ApiController
    {
        // The database context class which allows us to access the  MySQL Database.
        private SchoolDbContext schoodb = new SchoolDbContext();

        //This Controller Will access the class table of our schoodb database.
        /// <summary>
        /// Returns a list of Classes
        /// </summary>
        /// <example>GET api/Class/ClassList</example>
        /// <returns>
        /// A list of all the classes.
        /// </returns>
        [HttpGet]
        [Route("api/ClassData/ListClass/{SearchKey?}")]
        public IEnumerable<Class> ListClass(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = schoodb.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes where lower(classname) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Classes
            List<Class> Classes = new List<Class> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassCode = ResultSet["classcode"].ToString();
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]); ;
                string StartDate = ResultSet["startdate"].ToString();
                string FinishDate = ResultSet["finishdate"].ToString();
                string ClassName = ResultSet["classname"].ToString();



                Class NewClass = new Class();
                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.TeacherId = TeacherId;
                NewClass.StartDate = StartDate;
                NewClass.FinishDate = FinishDate;
                NewClass.ClassName = ClassName;


                //Add the Class Name to the List
                Classes.Add(NewClass);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of class names
            return Classes;
        }


        /// <summary>
        /// Returns an individual class from the database by the primary key classid
        /// </summary>
        /// <param name="id">the class's ID in the database</param>
        /// <returns>An class object</returns>
        [HttpGet]
        public Class FindClass(int id)
        {
            Class NewClass = new Class();

            //Create an instance of a connection
            MySqlConnection Conn = schoodb.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes where classid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassCode = ResultSet["classcode"].ToString();
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]); ;
                string StartDate = ResultSet["startdate"].ToString();
                string FinishDate = ResultSet["finishdate"].ToString();
                string ClassName = ResultSet["classname"].ToString();




                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.TeacherId = TeacherId;
                NewClass.StartDate = StartDate;
                NewClass.FinishDate = FinishDate;
                NewClass.ClassName = ClassName;

            }
            return NewClass;
        }
    }
}