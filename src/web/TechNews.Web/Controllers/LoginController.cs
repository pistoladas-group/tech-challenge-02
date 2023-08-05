using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TechBox.Web.Configurations;
using TechNews.Web.Models;

namespace TechNews.Web.Controllers;

[Route("[controller]")]
public class LoginController : Controller
{
    private readonly IHttpClientFactory _httpFactory;

    public LoginController(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("")]
    public async Task<IActionResult> SignInAsync([FromBody] SignInViewModel model)
    {
        var validationResult = ValidateRequest(model);

        // var result = new AppResult(); => TODO: Talvez renomear o model ApiResponse para AppResponse? Para usar aqui no front tb?

        if (!validationResult)
        {
            // TODO: Retornar erro
            return BadRequest();
        }

        var client = _httpFactory.CreateClient();

        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        var apiResponse = await client.PostAsync($"{Environment.GetEnvironmentVariable(EnvironmentVariables.ApiBaseUrl)}/api/auth/user/login", content);

        if (!apiResponse.IsSuccessStatusCode)
        {
            // TODO: Retornar um JSON pro JS (result)
            // para que seja possível avaliar:
            // Internal Error (500):
            //          - Mostrar mensagem de erro e pedir para contatar o suporte
            // Bad Request (400):
            //          - Mostrar mensagem "User or password are invalid"
            // Locked Out?:
            //          - Mostrar que usuário foi bloqueado e pedir contato com suporte

            return BadRequest();
        }

        // Login sucesso!!!
        // TODO: Avaliar melhor forma de guardar o token...
        // para que sempre seja passado para a API, e retornar um Json aqui

        return Ok();
    }

    private bool ValidateRequest(SignInViewModel model)
    {
        // TODO: Validar email e senha da mesma forma que faz na API
        return true;
    }
}