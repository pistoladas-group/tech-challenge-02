using System.Net;
using Microsoft.AspNetCore.Mvc;
using TechNews.Common.Library;

namespace TechNews.Core.Api.Controllers;

[ApiController]
[Route("api/news")]
public class NewsController : ControllerBase
{
    /// <summary>
    /// Get all news
    /// </summary>
    /// <response code="200">Returns the resource data</response>
    /// <response code="500">There was an internal problem</response>
    [HttpGet("")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAllNews()
    {
        return Ok(new ApiResponse());
    }

    /// <summary>
    /// Get news by id
    /// </summary>
    /// <response code="200">Returns the resource data</response>
    /// <response code="400">The resource was not found</response>
    /// <response code="404">There is a problem with the request</response>
    /// <response code="500">There was an internal problem</response>
    [HttpGet("{newsId:guid}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetNewsById([FromRoute] Guid newsId)
    {
        return Ok(new ApiResponse());
    }
}
