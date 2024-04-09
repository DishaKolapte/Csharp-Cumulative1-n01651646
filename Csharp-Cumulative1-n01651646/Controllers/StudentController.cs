using Csharp_Cumulative1_n01651646.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Csharp_Cumulative1_n01651646.Controllers
{
    public class StudentController : Controller
    {

            // GET: Student
            public ActionResult Index()
            {
                return View();
            }

            //GET : /Student/StudentList
            public ActionResult StudentList(string SearchKey = null)
            {
                StudentDataController controller = new StudentDataController();
                IEnumerable<Student> Students = controller.ListStudents(SearchKey);
                return View(Students);
            }

            //GET : /Student/StudentShow/{id}
            public ActionResult StudentShow(int id)
            {
                StudentDataController controller = new StudentDataController();
                Student NewStudent = controller.FindStudent(id);


                return View(NewStudent);
            }
        //POST : /Student/Create
        [HttpPost]
        public ActionResult Create(string StudentFname, string StudentLname, string StudentNumber, DateTime EnrolDate)
        {




            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(StudentFname);
            Debug.WriteLine(StudentLname);
            Debug.WriteLine(StudentNumber);
            Debug.WriteLine(EnrolDate);
            


            Student NewStudent = new Student(); ;
            NewStudent.StudentFname = StudentFname;
            NewStudent.StudentLname = StudentLname;
            NewStudent.StudentNumber = StudentNumber;
            NewStudent.EnrolDate = EnrolDate;
            

            StudentDataController controller = new StudentDataController();
            controller.AddStudent(NewStudent);




            return RedirectToAction("StudentList");
        }
        //GET : /Student/studentDelete/{id}
        public ActionResult studentDelete(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);


            return View(NewStudent);
        }


        //POST : /Student/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            StudentDataController controller = new StudentDataController();
            controller.DeleteStudent(id);
            return RedirectToAction("StudentList");
        }

        //GET : /Student/New
        public ActionResult addStudent()
        {
            return View();


        }
    }
    }
