using Microsoft.AspNetCore.Mvc;
using SmartEnergyAPI.Models;

namespace API_SmartEnergy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        /// <summary>
        /// Cadastra um novo cliente.
        /// </summary>
        /// <param name="cliente">Objeto contendo os dados do cliente a ser cadastrado.</param>
        /// <returns>O nome do cliente cadastrado.</returns>
        [HttpPost]
        public IActionResult Cadastrar([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Cadastro cadastro = new Cadastro();
                Cliente clienteCadastrado = cadastro.Cadastrar(cliente);
                return Ok(new { nome = clienteCadastrado.Nome });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = $"Erro ao cadastrar o cliente: {ex.Message}" });
            }
        }
    }
}
