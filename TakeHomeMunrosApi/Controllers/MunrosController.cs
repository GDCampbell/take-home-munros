using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using TakeHomeMunrosApi.Models;
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

        // GET: api/<MunrosController>
        [HttpGet]
        public IEnumerable<MunroModel> Get()
        {
            return munroService.GetMunros();
        }

        
    }
}
