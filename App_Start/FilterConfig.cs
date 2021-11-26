using System.Web;
using System.Web.Mvc;

namespace Midterm_Chuyende
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
