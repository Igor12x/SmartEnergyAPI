using API_SmartyEnergy.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartEnergyAPI.Models;

namespace API_SmartyEnergy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlterarCadastroController : ControllerBase
    {
        [HttpPut("{id}")]
        public IActionResult AlterarCliente([FromBody] AlterarCadastro dados, int id)
        {
            AlterarCadastro alterarCadastro = new AlterarCadastro(dados.Email, dados.Telefone);
            Cliente clienteAtualizado = alterarCadastro.Alterar(id);
            string json = JsonConvert.SerializeObject(clienteAtualizado);
            return Ok(json);
        }
    }
}
