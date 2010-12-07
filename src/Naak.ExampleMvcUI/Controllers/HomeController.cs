using System.Web.Mvc;

namespace Naak.ExampleMvcUI.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}