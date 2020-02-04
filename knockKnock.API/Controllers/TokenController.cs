using System.Threading.Tasks;
using knockKnock.API.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace knockKnock.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Gets my personal token.
        /// </summary>
        /// <returns>My Token</returns>
        [HttpGet]
        //[Route("Token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetToken()
        {
            var token = await _tokenService.SrvToken();
            return Ok(token);
        }

    }
}
