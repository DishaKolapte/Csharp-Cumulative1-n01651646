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
         
            MySqlConnection Conn = schoodb.AccessDatabase();

           
            Conn.Open();

          
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from classes where lower(classname) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

       
            MySqlDataReader ResultSet = cmd.ExecuteReader();

         
            List<Class> Classes = new List<Class> { };

         
            while (ResultSet.Read())
            {
               
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


                
                Classes.Add(NewClass);
            }

            //Closes the connection.
            Conn.Close();

            
            return Classes; //Returns the list of class names
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

         
            MySqlConnection Conn = schoodb.AccessDatabase();

            
            Conn.Open();

            
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes where classid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                
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