using Csharp_Cumulative1_n01651646.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Csharp_Cumulative1_n01651646.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Class/ClassList
        public ActionResult ClassList(string SearchKey = null)
        {
            ClassDataController controller = new ClassDataController();
            IEnumerable<Class> Classes = controller.ListClass(SearchKey);
            return View(Classes);
        }

        //GET : /Class/ClassShow/{id}
        public ActionResult ClassShow(int id)
        {
            ClassDataController controller = new ClassDataController();
            Class NewClass = controller.FindClass(id);


            return View(NewClass);
        }
    }
}