using System.Web;
using System.Web.Mvc;

namespace MockUp_Gruppe02_ITE1811
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
