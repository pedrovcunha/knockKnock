using System;
using System.Threading.Tasks;
using knockKnock.API.Models;
using knockKnock.API.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace knockKnock.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TriangleTypeController : ControllerBase
    {
        private readonly ITriangleTypeService _triangleTypeService;

        public TriangleTypeController(ITriangleTypeService triangleTypeService)
        {
            _triangleTypeService = triangleTypeService;
        }

        /// <summary>
        /// Get the type of triangle given the lengths of its sides.
        /// </summary>
        /// <param name="a">The length of side a</param>
        /// <param name="b">The length of side b</param>
        /// <param name="c">The length of side c</param>
        /// <returns>Returns the type of triangle given the lengths of its sides.</returns>
        [HttpGet]
        //[Route("TriangleType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTriangleType([FromQuery] int a, [FromQuery] int b, [FromQuery] int c)
        {
            var triangleType = await _triangleTypeService.SrvTriangleType(a, b, c);

            return Ok(Enum.GetName(typeof(Triangle.TriangleType), triangleType));
        }
    }
}
