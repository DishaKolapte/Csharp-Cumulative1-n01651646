using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Csharp_Cumulative1_n01651646.Models;
using Mysqlx.Datatypes;

namespace Csharp_Cumulative1_n01651646.Controllers
{
    public class TeacherController : Controller
    {

        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }
        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create( string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, string Salary)
        {

 


                Debug.WriteLine("I have accessed the Create Method!");
                Debug.WriteLine(TeacherFname);
                Debug.WriteLine(TeacherLname);
                Debug.WriteLine(EmployeeNumber);
                Debug.WriteLine(HireDate);
                Debug.WriteLine(Salary);


                Teacher NewTeacher = new Teacher(); ;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                TeacherDataController controller = new TeacherDataController();
                controller.AddTeacher(NewTeacher);
            



            return RedirectToAction("List");
        }
        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }


        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/New
        public ActionResult addNew()
        {
            return View();

            
        }

        //GET : /Teacher/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();

        }


    }
}