using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;

namespace Carmax.API.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class OidcConfigurationController : Controller
{
    private readonly ILogger<OidcConfigurationController> _logger;

    public OidcConfigurationController(
        IClientRequestParametersProvider clientRequestParametersProvider,
        ILogger<OidcConfigurationController> logger)
    {
        ClientRequestParametersProvider = clientRequestParametersProvider;
        _logger = logger;
    }

    public IClientRequestParametersProvider ClientRequestParametersProvider { get; }

    [HttpGet("_configuration/{userId}")]
    public IActionResult GetClientRequestParameters([FromRoute]string userId)
    {
        var parameters = ClientRequestParametersProvider.GetClientParameters(HttpContext, userId);
        return Ok(parameters);
    }
}
