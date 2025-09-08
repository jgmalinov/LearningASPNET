using Microsoft.AspNetCore.Mvc;

namespace RazorViews.Controllers
{
    public class HomeController : Controller
    {
        [Route("Home")]
        [Route("")]
        public IActionResult Index()
        {
            return View();
            // return View("AlternativeViewName");
            // return new ViewResult { ViewName = "AlternativeViewName" };
        }
    }
}
