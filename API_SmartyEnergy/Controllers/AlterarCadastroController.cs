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
        /// <summary>
        /// Altera os dados de cadastro de um cliente.
        /// </summary>
        /// <param name="dadosCliente">Objeto contendo os dados de alteração (Email e Telefone).</param>
        /// <param name="idCliente">ID do cliente a ser alterado.</param>
        /// <returns>O cliente atualizado.</returns>
        [HttpPut("{idCliente}")]
        public IActionResult AlterarCliente([FromBody] AlterarCadastro dadosCliente, int idCliente)
        {
            AlterarCadastro alterarCadastro = new AlterarCadastro(dadosCliente.Email, dadosCliente.Telefone);
            Cliente clienteAtualizado = alterarCadastro.Alterar(idCliente);
            string json = JsonConvert.SerializeObject(clienteAtualizado);
            return Ok(json);
        }
    }
}
