using Microsoft.AspNetCore.Mvc;

namespace TechNews.Web.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public LoginController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}
