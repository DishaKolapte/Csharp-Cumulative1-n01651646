using Csharp_Cumulative1_n01651646.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

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
                DateTime EnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);




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
                DateTime EnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);




                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;

            }
            return NewStudent;
        }
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddStudent([FromBody] Student NewStudent)
        {
            if (!NewStudent.IsValid()) return;  // Server side validation. 

            //Create an instance of a connection
            MySqlConnection Conn = schoodb.AccessDatabase();

            Debug.WriteLine(NewStudent.StudentFname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into students (studentfname, studentlname, studentnumber, enroldate) values (@StudentFname,@StudentLname,@StudentNumber, @EnrolDate)";
            cmd.Parameters.AddWithValue("@StudentFname", NewStudent.StudentFname);
            cmd.Parameters.AddWithValue("@StudentLname", NewStudent.StudentLname);
            cmd.Parameters.AddWithValue("@StudentNumber", NewStudent.StudentNumber);
            cmd.Parameters.AddWithValue("@Enroldate", NewStudent.EnrolDate);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();



        }
        /// <summary>
        /// Deletes an Student from the connected MySQL Database if the ID of that author exists. Does NOT maintain relational integrity.
        /// </summary>
        /// <param name="id">The ID of the author.</param>
        /// <example>POST /api/StudentData/DeleteStudent/3</example>
        [HttpPost]
        public void DeleteStudent(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = schoodb.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from students where studentid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }
    }
}