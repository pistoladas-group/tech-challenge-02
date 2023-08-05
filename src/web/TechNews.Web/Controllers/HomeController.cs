using Microsoft.AspNetCore.Mvc;

namespace TechNews.Web.Controllers;

public class HomeController : Controller
{
    public HomeController()
    {
    }

    public IActionResult Index()
    {
        // TODO: Está logado?: 
        // SIM: Ir para a home mostrando o nome de usuário "Olá! lalal" e um botão de Sair
        // NÃO: Ir para a home mostrando o botão de Entrar
        return View();
    }
}
