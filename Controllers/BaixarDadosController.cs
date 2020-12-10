using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DesafioMutant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaixarDadosController : ControllerBase
    {
        private readonly ILogger<BaixarDadosController> _logger;

        public BaixarDadosController(ILogger<BaixarDadosController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Models.User>> Get()
        {
            Connection c = new Connection();
            
            return c.getListUser();

        }
    }
}
