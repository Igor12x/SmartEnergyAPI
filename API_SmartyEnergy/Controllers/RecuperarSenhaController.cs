using API_SmartyEnergy.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_SmartyEnergy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecuperarSenhaController : ControllerBase
    {

        [HttpGet("CodigoVerificacao/{email}")]
        public IActionResult EnviarCodigoVerificacao(string email)
        {
            string codigoVericacao = RecuperarSenha.EnviarCodigoVerificacao(email);
            return Ok(codigoVericacao);
        }
    }
}
