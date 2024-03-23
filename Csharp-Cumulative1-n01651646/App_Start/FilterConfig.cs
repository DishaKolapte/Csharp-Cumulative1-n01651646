using System.Web;
using System.Web.Mvc;

namespace Csharp_Cumulative1_n01651646
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
