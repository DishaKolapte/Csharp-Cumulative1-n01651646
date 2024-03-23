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
    public class StudentDataController : ApiController
    {
        // The database context class which allows us to access the MySQL Database.
        private SchoolDbContext schoodb = new SchoolDbContext();

        //This Controller Will access the students table of our schoodb database.
        /// <summary>
        /// Returns a list of Students.
        /// </summary>
        /// <example>GET api/Student/StudentList</example>
        /// <returns>
        /// A list of all the students (their first and last names).
        /// </returns>
        [HttpGet]
        [Route("api/StudentData/ListStudents/{SearchKey?}")]
        public IEnumerable<Student> ListStudents(string SearchKey = null)
        {
            
            MySqlConnection Conn = schoodb.AccessDatabase();

            //Instance My Sql connection opened.
            Conn.Open(); 

            // new query for the schoodb database.
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Students where lower(studentfname) like lower(@key) or lower(studentlname) like lower(@key) or lower(concat(studentfname, ' ', studentlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            
            List<Student> Students = new List<Student> { };

            
            while (ResultSet.Read())
            {
                
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                string EnrolDate = ResultSet["enroldate"].ToString();



                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;


                //Add the Student Name to the List
                Students.Add(NewStudent);
            }

            //Close instance MySql connection.
            Conn.Close(); 

            
            return Students;  //Returns the list of Student names.
        }


        /// <summary>
        /// Returns an individual student from the database by the primary key (studentid)
        /// </summary>
        /// <param name="id">the student's ID in the database</param>
        /// <returns>A student object.</returns>
        [HttpGet]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            
            MySqlConnection Conn = schoodb.AccessDatabase();  // creates My Sql Instance connection. 

            //Open the connection.
            Conn.Open();

            
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Students where studentid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                string EnrolDate = ResultSet["enroldate"].ToString();




                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;

            }
            return NewStudent;
        }
    }
}