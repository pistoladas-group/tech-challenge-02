using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TechNews.Web.Configurations;
using TechNews.Web.Models;

namespace TechNews.Web.Controllers;

[Route("[controller]")]
public class AccountController : Controller
{
    private readonly IHttpClientFactory _httpFactory;

    public AccountController(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("")]
    public async Task<IActionResult> SignUpAsync([FromBody] SignUpViewModel model)
    {
        var client = _httpFactory.CreateClient();

        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        var apiResponse = await client.PostAsync($"{Environment.GetEnvironmentVariable(EnvironmentVariables.ApiBaseUrl)}/api/auth/user", content);

        if (!apiResponse.IsSuccessStatusCode)
        {
            if (apiResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResponse = await apiResponse.Content.ReadAsStringAsync();

                // TODO: Culturizar as mensagens (traduzir)
                var appResponse = JsonSerializer.Deserialize<AppResponseModel>(errorResponse, new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return BadRequest(appResponse);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        // TODO: Avaliar melhor forma de guardar o token...
        // para que sempre seja passado para a API de Core
        // pegar username do JWT e passar pro JS...

        return Ok(new AppResponseModel(data: "Mock"));
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View("Login");
    }

    [HttpPost("login")]
    public async Task<IActionResult> SignInAsync([FromBody] SignInViewModel model)
    {
        var client = _httpFactory.CreateClient();

        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        var apiResponse = await client.PostAsync($"{Environment.GetEnvironmentVariable(EnvironmentVariables.ApiBaseUrl)}/api/auth/user/login", content);

        if (!apiResponse.IsSuccessStatusCode)
        {
            if (apiResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(new AppResponseModel("Usuário ou senha inválidos"));
            }

            if (apiResponse.StatusCode == HttpStatusCode.InternalServerError ||
                apiResponse.StatusCode == HttpStatusCode.Forbidden)
            {
                return StatusCode((int)apiResponse.StatusCode);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        // TODO: Avaliar melhor forma de guardar o token...
        // para que sempre seja passado para a API de Core
        // pegar username do JWT e passar pro JS...

        return Ok(new AppResponseModel(data: "Mock"));
    }

    [HttpGet("logout")]
    public IActionResult LogOut()
    {
        return NotFound();
    }
}
