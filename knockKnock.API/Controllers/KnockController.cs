using System;
using knockKnock.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace knockKnock.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    public class KnockController : ControllerBase
    {
        private readonly ILogger<KnockController> _logger;
        private readonly IKnockService _knockService;

        public KnockController(ILogger<KnockController> logger, IKnockService knockService)
        {
            _logger = logger;
            _knockService = knockService;
        }

        /// <summary>
        /// Get the nth number in the fibonacci sequence.
        /// </summary>
        /// <param name="n">The index (n) of the fibonacci sequence</param>
        /// <returns>Returns the nth Fibonacci number.</returns>
        [HttpGet]
        [Route("Fibonacci")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetFibonacciNumber([FromQuery] long n)
        {
            try
            {
                var fibonacciNumber = _knockService.SvrFibonacci(n);
                return Ok(fibonacciNumber);
            }
            catch (Exception)
            {
                return UnprocessableEntity();
            }
        }

        /// <summary>
        /// Get the Reverse Words
        /// </summary>
        /// <param name="sentence">Sequence of characters to be reverse (Phrase/Sentence).</param>
        /// <returns>Reverses the letters of each word in a sentence.</returns>
        [HttpGet]
        [Route("ReverseWords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetReverseWords([FromQuery] string sentence)
        {
            try
            {
                var reverseSentence = _knockService.SvrReverseWord(sentence);
                return Ok(reverseSentence);
            }
            catch (Exception e)
            {
                return UnprocessableEntity();
            }
        }

        /// <summary>
        /// Gets my personal token.
        /// </summary>
        /// <returns>My Token</returns>
        [HttpGet]
        [Route("Token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetToken()
        {
            var token = _knockService.SrvToken();
            return Ok(token);
        }

        /// <summary>
        /// Get the type of triangle given the lengths of its sides.
        /// </summary>
        /// <param name="a">The length of side a</param>
        /// <param name="b">The length of side b</param>
        /// <param name="c">The length of side c</param>
        /// <returns>Returns the type of triangle given the lengths of its sides.</returns>
        [HttpGet]
        [Route("TriangleType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetTriangleType([FromQuery] int a, [FromQuery] int b, [FromQuery] int c)
        {
            var triangleType = _knockService.SrvTriangleType(a, b, c);
            
            return Ok(Enum.GetName(typeof(KnockService.TriangleType),triangleType));
        }
    }
}
