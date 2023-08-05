using System.Net;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TechNews.Auth.Api.Data;
using TechNews.Auth.Api.Models;
using TechNews.Common.Library;
using TechNews.Auth.Api.Services;

namespace TechNews.Auth.Api.Controllers;

[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RsaTokenSigner _rsaTokenSigner;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, RsaTokenSigner crypto)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _rsaTokenSigner = crypto;
    }

    /// <summary>
    /// Creates a new User
    /// </summary>
    /// <param name="user">The user to be registered</param>
    /// <response code="201">Returns the created resource endpoint in response header</response>
    /// <response code="400">There is a problem with the request</response>
    /// <response code="500">There was an internal problem</response>
    [HttpPost("user")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequestModel user)
    {
        var id = user.Id ?? Guid.NewGuid();

        //TODO: verificar se j� existe algum usu�rio com aquele id, para evitar exception

        var createUserResult = await _userManager.CreateAsync(new User(id, user.Email, user.UserName), user.Password);

        if (!createUserResult.Succeeded)
        {
            return BadRequest(new ApiResponse(errors: createUserResult.Errors.ToList().ConvertAll(x => x.Description)));
        }

        return CreatedAtAction(nameof(GetUser), new { userId = id }, new ApiResponse());
    }

    /// <summary>
    /// Get the user details
    /// </summary>
    /// <param name="userId">The user id to be searched</param>
    /// <response code="200">Returns the resource data</response>
    /// <response code="400">There is a problem with the request</response>
    /// <response code="404">There is no resource with the given id</response>
    /// <response code="500">There was an internal problem</response>
    [HttpGet("user/{userId:guid}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return BadRequest(new ApiResponse(error: "The userId is not valid"));
        }

        var getUserResult = await _userManager.FindByIdAsync(userId.ToString());

        if (getUserResult == null)
        {
            return NotFound(new ApiResponse(error: "The user was not found"));
        }

        var responseModel = new GetUserResponseModel
        {
            Id = getUserResult.Id,
            UserName = getUserResult.UserName,
            Email = getUserResult.Email,
        };

        return Ok(new ApiResponse(data: responseModel));
    }

    /// <summary>
    /// Login an user
    /// </summary>
    /// <param name="user">The user to be logged in</param>
    /// <response code="201">Returns the user login data</response>
    /// <response code="400">There is a problem with the request</response>
    /// <response code="404">The informed user was not found</response>
    /// <response code="500">There was an internal problem</response>
    [HttpPost("user/login")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel user)
    {
        var registeredUserResult = await _userManager.FindByEmailAsync(user.Email);

        if (registeredUserResult?.UserName is null)
        {
            return BadRequest(new ApiResponse(error: "User or password are invalid"));
        }

        var signInResult = await _signInManager.PasswordSignInAsync(registeredUserResult.UserName, user.Password, false, true);

        if (signInResult.IsLockedOut)
        {
            return BadRequest(new ApiResponse(error: "User temporary blocked for invalid attempts"));
        }

        if (!signInResult.Succeeded)
        {
            return BadRequest(new ApiResponse(error: "User or password are invalid"));
        }

        var claims = await GetUserClaims(registeredUserResult);

        //TODO: Se token for nulo, retornar 500 Internal Server Error
        var token = GetToken(claims, registeredUserResult);

        return Ok(new ApiResponse(data: token));
    }

    private string? GetToken(ClaimsIdentity claims, User user)
    {
        var tokenClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
            new(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)
        };

        claims.AddClaims(tokenClaims);

        //TODO: pegar dinamicamente (https) e (host):
        const string currentIssuer = "https://localhost:7279";

        var signingCredentials = _rsaTokenSigner.GetSigningCredentials();

        //TODO: Se signingCredentials for nulo, retornar nulo string? (quer dizer que não existe chave para assinar o token)

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = currentIssuer,
            Subject = claims,
            Expires = DateTime.UtcNow.AddMinutes(10), //TODO: tirar hardcode
            TokenType = "at+jwt",
            SigningCredentials = signingCredentials
        });

        return tokenHandler.WriteToken(token);
    }

    private static long ToUnixEpochDate(DateTime date)
    {
        return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }

    private async Task<ClaimsIdentity> GetUserClaims(User registeredUserResult)
    {
        var claims = new ClaimsIdentity();
        var userClaims = await _userManager.GetClaimsAsync(registeredUserResult);
        var userRoles = await _userManager.GetRolesAsync(registeredUserResult);

        claims.AddClaims(userClaims);
        foreach (var userRole in userRoles)
        {
            claims.AddClaim(new Claim("role", userRole));
        }

        return claims;
    }
}