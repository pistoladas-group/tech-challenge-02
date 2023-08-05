using Microsoft.AspNetCore.Mvc;

namespace TechNews.Web.Controllers;

public class HomeController : Controller
{
    public HomeController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}
