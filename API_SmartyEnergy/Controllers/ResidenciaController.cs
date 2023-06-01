using API_SmartyEnergy.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API_SmartyEnergy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidenciaController : ControllerBase
    {
        [HttpGet("listarResidencias/{id}")]
        public IActionResult ListarResidencias(int id)
        {
            var residencias = Residencia.Listar(id);
            string json = JsonConvert.SerializeObject(residencias);
            return Ok(json);
        }
    }
}
