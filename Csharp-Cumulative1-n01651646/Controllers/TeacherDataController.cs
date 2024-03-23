using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Csharp_Cumulative1_n01651646.Models;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;



namespace Csharp_Cumulative1_n01651646.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext schoodb = new SchoolDbContext();

        //This Controller Will access the teachers table of our schoodb database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/Teacher/List</example>
        /// <returns>
        /// A list of teacher objects.
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {

            MySqlConnection Conn = schoodb.AccessDatabase();


            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

 
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Teacher> Teachers = new List<Teacher> { }; //Creates teachers list


            while (ResultSet.Read())
            {

                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                string HireDate = ResultSet["hiredate"].ToString();
                string Salary = ResultSet["salary"].ToString();


                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                //Adds the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            //Closes the connection
            Conn.Close();


            return Teachers;  //Returns the list of teachers names.
        }


        /// <summary>
        /// Returns an individual teacher from the database by specifying the primary key teacherid
        /// </summary>
        /// <param name="id">the teacher's ID in the database</param>
        /// <returns>An teacher object.</returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();


            MySqlConnection Conn = schoodb.AccessDatabase();


            Conn.Open();

   
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

   
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
        
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                string HireDate = ResultSet["hiredate"].ToString();
                string Salary = ResultSet["salary"].ToString();



                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

            }


            return NewTeacher;

        }
    }
}
