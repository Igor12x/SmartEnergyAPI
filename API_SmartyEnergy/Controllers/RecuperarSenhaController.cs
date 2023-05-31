﻿using API_SmartyEnergy.Models;
using Microsoft.AspNetCore.Mvc;
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
            string codigoVerificacao = RecuperarSenha.EnviarCodigoVerificacao(email);
            return Ok(codigoVerificacao);
        }
        
        [HttpPut("RedefinirSenha")]
        public IActionResult AlterarSenhaCliente([FromBody] Cliente cliente)
        {            
            return Ok(RecuperarSenha.RedefinirSenha(cliente));
        }
    }
}
