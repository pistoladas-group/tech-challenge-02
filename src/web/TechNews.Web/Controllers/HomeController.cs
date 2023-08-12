using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechNews.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    public HomeController()
    {
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }
}
