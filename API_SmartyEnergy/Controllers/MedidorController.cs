
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using API_SmartyEnergy.Models;

namespace SmarEnergy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedidorController : ControllerBase
    {

        // GET: api/<ArduinoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ArduinoController>/5
        [HttpGet("BuscarConsumo/{id}")]

        public IActionResult BuscarConsumo(int id)
        {
            /*transformando um array em um Json no formarto string, pois é possível converter para um JArray
            um objeto do tipo Json*/
            string json = JsonConvert.SerializeObject(Medidor.buscarConsumo(id)); //provisóriamente o id que pretendemos receber é o código, mas deve mudar para chave estrangeira resdiência
            return Ok(json); // codigo 200 sucesso
        }

        [HttpGet("BuscarConsumoDiario/{id}")]
        public IActionResult BuscarConsumoDiario(int id)
        {
            /*transformando um array em um Json no formarto string, pois é possível converter para um JArray
            um objeto do tipo Json*/

            string json = JsonConvert.SerializeObject(Medidor.buscarConsumoDiario(id)); //provisóriamente o id que pretendemos receber é o código, mas deve mudar para chava estrangeira resdiência
            return Ok(json); // codigo 200 sucesso
        }

        // POST api/<ArduinoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ArduinoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArduinoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

