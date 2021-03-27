using Microsoft.AspNetCore.Mvc;

using TakeHomeMunrosApi.Queries;
using TakeHomeMunrosApi.Services;

namespace TakeHomeMunrosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunrosController : ControllerBase
    {
        readonly MunroService munroService;

        public MunrosController(MunroService munroService)
        {
            this.munroService = munroService;
        }

        // GET: api/<MunrosController>/
        [HttpGet]
        public IActionResult Get([FromQuery]MunroQuery sortQuery)
        {
            if ((sortQuery?.MinHeightInMetres ?? 0) > (sortQuery?.MaxHeightInMetres ?? double.MaxValue))
            {
                return BadRequest("Provided maximum height should be greater than the provided minimum height or zero.");
            }

            return Ok(munroService.GetMunros(sortQuery));
        }

        
    }
}
