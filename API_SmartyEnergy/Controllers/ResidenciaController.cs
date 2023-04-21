using API_SmartyEnergy.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_SmartyEnergy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidenciaController : ControllerBase
    {
        // GET: api/<ResidenciaController>
        [HttpGet("listarResidencias/{id}")]
        public IActionResult listarResidencias(int id)
        {
            string json = JsonConvert.SerializeObject(Residencia.listar(id));
            return Ok(json);
        }
    }
}
