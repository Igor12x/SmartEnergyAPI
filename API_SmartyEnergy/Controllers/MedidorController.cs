
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using API_SmartyEnergy.Models;

namespace SmarEnergy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedidorController : ControllerBase
    {

        // GET api/<ArduinoController>/5
        [HttpGet("BuscarConsumo/{id}")]

        public IActionResult BuscarConsumo(int id)
        {
            string json = JsonConvert.SerializeObject(Medidor.buscarConsumo(id));
            return Ok(json);
        }

        [HttpGet("BuscarConsumoDiario/{id}")]
        public IActionResult BuscarConsumoDiario(int id)
        {
            string json = JsonConvert.SerializeObject(Medidor.buscarConsumoDiario(id));
            return Ok(json);
        }
        
        [HttpPost("GravarConsumo")]
        public void GravarConsumo([FromBody] Medidor medidorResidencia)
        {
            Medidor.GravarConsumo(medidorResidencia);
        }
    }
}

