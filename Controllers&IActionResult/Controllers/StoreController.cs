using Microsoft.AspNetCore.Mvc;

namespace Controllers_IActionResult.Controllers
{
    public class StoreController : Controller
    {
        [Route("/store/books")]
        public IActionResult Book()
        {
            return File("/ExampleRelative.txt", "text/plain");
        }
    }
}
