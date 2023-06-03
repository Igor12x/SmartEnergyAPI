    using API_SmartyEnergy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.WebApi.Jwt;
using SmartEnergyAPI.Models;

namespace API_SmartyEnergy.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class RecuperarSenhaController : ControllerBase {
        /// <summary>
        /// Envia um código de verificação para o email fornecido.
        /// </summary>
        /// <param name="email">O email do cliente.</param>
        /// <returns>O código de verificação.</returns>
        [HttpGet("CodigoVerificacao/{email}")]
        public IActionResult EnviarCodigoVerificacao(string email) {
            try {
                string codigoVerificacao = RecuperarSenha.EnviarCodigoVerificacao(email);
                return Ok(codigoVerificacao);
            } catch (InvalidCredentialsException ex) {
                return BadRequest(ex.Message);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Redefine a senha do cliente.
        /// </summary>
        /// <param name="novaSenhaCliente">Os dados para redefinição da senha.</param>
        /// <returns>O resultado da operação de redefinição de senha.</returns>
        [HttpPut("RedefinirSenha")]
        public IActionResult RedefinirSenha([FromBody] RecuperarSenha novaSenhaCliente) {
            try {
                string resultado = novaSenhaCliente.RedefinirSenha(novaSenhaCliente);
                return Ok(new { retorno = resultado });
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
