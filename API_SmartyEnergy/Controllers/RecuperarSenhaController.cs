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
        private readonly RecuperarSenha _recuperarSenha;

        public RecuperarSenhaController(RecuperarSenha recuperarSenha)
        {
            _recuperarSenha = recuperarSenha;
        }

        [HttpGet("CodigoVerificacao/{email}")]
        public IActionResult EnviarCodigoVerificacao(string email)
        {
            try
            {
                string codigoVerificacao = _recuperarSenha.EnviarCodigoVerificacao(email);
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
        public IActionResult RedefinirSenha([FromBody] Cliente cliente)
        {
            try
            {
                string resultado = _recuperarSenha.RedefinirSenha(cliente);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
