﻿using Microsoft.AspNetCore.Mvc;

namespace TechNews.Web.Controllers;

public class LoginController : Controller
{

    public LoginController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}
