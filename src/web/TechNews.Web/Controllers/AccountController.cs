using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechNews.Web.Configurations;
using TechNews.Web.Models;

namespace TechNews.Web.Controllers;

[Authorize]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly IHttpClientFactory _httpFactory;

    public AccountController(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    [AllowAnonymous]
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [AllowAnonymous]
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
                var response = JsonSerializer.Deserialize<AppResponseModel>(errorResponse, new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return BadRequest(response);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        var responseString = await apiResponse.Content.ReadAsStringAsync();
        var appResponse = JsonSerializer.Deserialize<AppResponseModel>(responseString, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        var token = GetTokenFromString(appResponse?.Data?.ToString());

        if (token is null)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        await AuthenticateUserByTokenAsync(token);

        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("login")]
    public IActionResult Login()
    {
        return View("Login");
    }

    [AllowAnonymous]
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

        var responseString = await apiResponse.Content.ReadAsStringAsync();
        var appResponse = JsonSerializer.Deserialize<AppResponseModel>(responseString, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        var token = GetTokenFromString(appResponse?.Data?.ToString());

        if (token is null)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        await AuthenticateUserByTokenAsync(token);

        return Ok();
    }

    [HttpGet("logout")]
    public async Task<IActionResult> LogOutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Redirect("Login");
    }

    private static JwtSecurityToken? GetTokenFromString(string? jwtToken)
    {
        return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
    }

    private async Task AuthenticateUserByTokenAsync(JwtSecurityToken token)
    {
        var claimsIdentity = GetClaimsByJwt(token);

        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20), //TODO: Tirar hardcode
            IsPersistent = true
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
    }

    private static ClaimsIdentity GetClaimsByJwt(JwtSecurityToken token)
    {
        var claims = new List<Claim>();
        claims.AddRange(token.Claims);
        claims.Add(new Claim("JWT", "Access Token JWT"));

        return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
