using Microsoft.AspNetCore.Mvc;

namespace Views_Part2.Controllers
{
    public class ProductController : Controller
    {
        [Route("/products")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/products/create")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("/products/search/{id:int?}")]
        public IActionResult Search(int? id)
        {
            ViewBag.ProductId = id;
            return View();
        }
    }
}
