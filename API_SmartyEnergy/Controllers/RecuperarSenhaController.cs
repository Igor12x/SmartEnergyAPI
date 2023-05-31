using API_SmartyEnergy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.WebApi.Jwt;
using SmartEnergyAPI.Models;

namespace API_SmartyEnergy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecuperarSenhaController : ControllerBase
    {
        [HttpGet("CodigoVerificacao/{email}")]
        public IActionResult EnviarCodigoVerificacao(string email)
        {
            try
            {
                string codigoVerificacao = RecuperarSenha.EnviarCodigoVerificacao(email);
                return Ok(codigoVerificacao);
            }
            catch (InvalidCredentialsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("RedefinirSenha")]
        public IActionResult RedefinirSenha([FromBody] RecuperarSenha novaSenhaCliente)
        {
            try
            {
                string resultado = novaSenhaCliente.RedefinirSenha(novaSenhaCliente);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
