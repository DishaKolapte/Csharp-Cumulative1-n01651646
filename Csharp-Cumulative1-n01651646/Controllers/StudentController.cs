using Csharp_Cumulative1_n01651646.Models;
using System;
using System.Collections.Generic;
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
        }
    }
