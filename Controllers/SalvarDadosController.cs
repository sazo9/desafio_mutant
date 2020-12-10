using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace DesafioMutant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalvarDadosController : ControllerBase
    {
        private readonly ILogger<SalvarDadosController> _logger;

        public SalvarDadosController(ILogger<SalvarDadosController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            Connection c = new Connection();
            c.save();
            return "";
        }
    }
}
