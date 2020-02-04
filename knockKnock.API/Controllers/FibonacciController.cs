using System;
using System.Threading.Tasks;
using knockKnock.API.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace knockKnock.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FibonacciController : Controller
    {
        private readonly ILogger<FibonacciController> _logger;
        private readonly IFibonacciService _fibonacciService;

        public FibonacciController(ILogger<FibonacciController> logger, IFibonacciService fibonacciService)
        {
            _logger = logger;
            _fibonacciService = fibonacciService;
        }

        /// <summary>
        /// Get the nth number in the fibonacci sequence.
        /// </summary>
        /// <param name="n">The index (n) of the fibonacci sequence</param>
        /// <returns>Returns the nth Fibonacci number.</returns>
        [HttpGet]
        //[Route("Fibonacci")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFibonacciNumberAsync([FromQuery] long n)
        {
            try
            {
                var fibonacciNumber = await _fibonacciService.SvrFibonacci(n);
                return Ok(fibonacciNumber);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
