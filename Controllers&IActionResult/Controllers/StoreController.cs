using Microsoft.AspNetCore.Mvc;

namespace Controllers_IActionResult.Controllers
{
    public class StoreController : Controller
    {
        [Route("/store/book/{id}")]
        public IActionResult Book()
        {
            var id = Convert.ToInt32(Request.RouteValues["id"]);
            Console.WriteLine(id);
            return File("/ExampleRelative.txt", "text/plain");
        }
    }
}
