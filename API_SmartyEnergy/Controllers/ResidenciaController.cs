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
        [HttpGet("buscar/{id}")]
        public IActionResult buscarCompanhia(int id)
        {
            string json = JsonConvert.SerializeObject(Residencia.listarResidencias(id));
            return Ok(json);
        }


        // GET api/<ResidenciaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ResidenciaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ResidenciaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ResidenciaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
