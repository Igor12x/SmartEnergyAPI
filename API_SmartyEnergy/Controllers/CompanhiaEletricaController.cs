using API_SmartyEnergy.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_SmartyEnergy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanhiaEletricaController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet ("buscarCompanhia/{id}")]
        public IActionResult buscarCompanhia(int id)
        {
            string json = JsonConvert.SerializeObject(CompanhiaEletrica.buscar(id));
            return Ok(json);
        }
    }
}
