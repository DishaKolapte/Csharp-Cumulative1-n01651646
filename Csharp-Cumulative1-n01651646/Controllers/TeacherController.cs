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

        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the teacher and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher/Update/5</example>
        public ActionResult Update(int id)
        {
            try
            {
                TeacherDataController controller = new TeacherDataController();
                Teacher SelectedTeacher = controller.FindTeacher(id);
                return View(SelectedTeacher);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }

        }

        /// <summary>
        /// Routes to a dynamically rendered "Ajax Update" Page. The "Ajax Update" page will utilize JavaScript to send an HTTP Request to the data access layer (/api/TeacherData/UpdateTeacher)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Ajax_Update(int id)
        {
            try
            {
                TeacherDataController controller = new TeacherDataController();
                Teacher SelectedTeacher =controller.FindTeacher(id);
                return View(SelectedTeacher);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }


        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="EmployeeNumber">The update employee number for the teacher.</param>
        /// <param name="HireDate">The updated hiredate of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id,string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, string Salary)
        {
            try
            {
                TeacherDataController controller = new TeacherDataController();
                Teacher TeacherInfo = new Teacher();
                TeacherInfo.TeacherFname = TeacherFname;
                TeacherInfo.TeacherLname = TeacherLname;
                TeacherInfo.EmployeeNumber = EmployeeNumber;
                TeacherInfo.HireDate = HireDate;
                TeacherInfo.Salary = Salary;

                controller.UpdateTeacher(id, TeacherInfo);
                Debug.WriteLine("try");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                Debug.WriteLine("catch");
                return RedirectToAction("Error", "Home");
                
            }


            return RedirectToAction("Show/" +id);
        }

    }
}


   