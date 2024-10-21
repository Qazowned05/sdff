using System.Web;
using System.Web.Mvc;

namespace Proyecto_final_Rivera_Paz_Diego_Eduardo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
