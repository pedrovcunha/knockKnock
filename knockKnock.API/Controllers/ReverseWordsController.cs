using System;
using System.Threading.Tasks;
using knockKnock.API.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace knockKnock.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController] 
    public class ReverseWordsController : ControllerBase
    {
        private readonly IReverseWordService _reverseWordService;

        public ReverseWordsController(IReverseWordService reverseWordService)
        {
            _reverseWordService = reverseWordService;
        }

        /// <summary>
        /// Get the Reverse Words
        /// </summary>
        /// <param name="sentence">Sequence of characters to be reverse (Phrase/Sentence).</param>
        /// <returns>Reverses the letters of each word in a sentence.</returns>
        [HttpGet]
        //[Route("ReverseWords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReverseWordsAsync([FromQuery] string sentence)
        {
            try
            {
                var reverseSentence = await _reverseWordService.SvrReverseWord(sentence);
                return Ok(reverseSentence);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
