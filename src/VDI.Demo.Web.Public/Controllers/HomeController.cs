using Microsoft.AspNetCore.Mvc;
using VDI.Demo.Web.Controllers;

namespace VDI.Demo.Web.Public.Controllers
{
    public class HomeController : DemoControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}