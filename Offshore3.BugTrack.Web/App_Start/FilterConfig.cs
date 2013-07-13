using System.Web;
using System.Web.Mvc;

namespace Offshore3.BugTrack.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}