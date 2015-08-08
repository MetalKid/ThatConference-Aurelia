using System.Web.Mvc;
using ThatConference.Northwind.Web.Mvc.Attributes;

namespace ThatConference.Northwind.Web.Mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
