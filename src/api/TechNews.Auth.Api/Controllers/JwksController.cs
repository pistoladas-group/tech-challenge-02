using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TechNews.Auth.Api.Models;
using TechNews.Common.Library;
using TechNews.Auth.Api.Services;

namespace TechNews.Auth.Api.Controllers;

[Route("jwks")]
public class JwksController : ControllerBase
{
    private readonly ICryptographicKeyRetriever _cryptographicKeyRetriever;

    public JwksController(ICryptographicKeyRetriever rsaKeyRetriever)
    {
        _cryptographicKeyRetriever = rsaKeyRetriever;
    }

    /// <summary>
    /// Gets the Json Web Key Sets
    /// </summary>
    /// <response code="200">Returns the Json Web Key Sets if any</response>
    /// <response code="500">There was an internal problem</response>
    [HttpGet()]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public IActionResult GetJsonWebKeySets()
    {
        var keys = _cryptographicKeyRetriever.GetLastKeys(2);

        var result = new JwksResponseModel();

        if (keys is null)
        {
            return Ok(result);
        }

        foreach (var key in keys)
        {
            var publicParameters = key.Instance.ExportParameters(false);

            result.Keys.Add(new JsonWebKeyModel()
            {
                KeyType = "RSA", // TODO: Deixar dinâmico
                KeyId = key.Id.ToString(),
                Algorithm = "RS256",
                Use = "sig",
                Modulus = Base64UrlEncoder.Encode(publicParameters.Modulus),
                Exponent = Base64UrlEncoder.Encode(publicParameters.Exponent)
            });
        }

        return Ok(result);
    }
}