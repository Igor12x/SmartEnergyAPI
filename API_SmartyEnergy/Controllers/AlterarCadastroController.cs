using API_SmartyEnergy.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartEnergyAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_SmartyEnergy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlterarCadastroController : ControllerBase
    {
        [HttpPut("{id}")]
        public IActionResult AlterarCliente([FromBody] AlterarCadastro dados, int id)
        {

            Cliente clienteAtualizado = AlterarCadastro.Alterar(dados, id);
            string json = JsonConvert.SerializeObject(clienteAtualizado);
            return Ok(json);
        }
    }
}
