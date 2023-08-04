using Microsoft.AspNetCore.Mvc;

namespace TechNews.Web.Controllers;

public class AccountController : Controller
{
    public AccountController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}
