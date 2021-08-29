using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BloggyWebAssembly.Server.Controllers
{
    public class OidcConfigurationController : Controller
    {
        private readonly ILogger<OidcConfigurationController> _logger;

        public IClientRequestParametersProvider ClientRequestParametersProvider { get; }

        public OidcConfigurationController(IClientRequestParametersProvider clientRequestParametersProvider, ILogger<OidcConfigurationController> logger)
        {
            ClientRequestParametersProvider = clientRequestParametersProvider;
            _logger = logger;
        }

        [HttpGet("_configuration/{clientID}")]
        public IActionResult GetClientRequestParameters([FromRoute] string clientID)
        {
            var parameters = ClientRequestParametersProvider.GetClientParameters(HttpContext, clientID);
            return Ok(parameters);
        }
    }
}
