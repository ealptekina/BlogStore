using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult ArticleDetail(int id)
        {
            ViewBag.i = id;
            return View();
        }

        public IActionResult ArticleList(int id)
        {
           //var values = 
            return View();
        }
    }
}
