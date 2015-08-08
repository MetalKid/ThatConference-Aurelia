using System.Web.Mvc;
using ThatConference.Northwind.Interfaces.Services.Northwind;

namespace ThatConference.Northwind.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeesService _currencyService;

        public HomeController(IEmployeesService currencyService)
        {
            _currencyService = currencyService;
        }

        public ActionResult Index()
        {
            var currencies = _currencyService.Get();

            return View();
        }
    }
}