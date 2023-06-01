using API_SmartyEnergy.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace API_SmartyEnergy.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CompanhiaEletricaController : ControllerBase {
        /// <summary>
        /// Busca uma companhia elétrica pelo ID da Residência.
        /// </summary>
        /// <param name="idResidencia">ID da Residência.</param>
        /// <returns>Os dados da companhia elétrica.</returns>
        [HttpGet("BuscarCompanhia/{idResidencia}")]
        public IActionResult BuscarCompanhia(int idResidencia) {
            try {
                CompanhiaEletrica companhiaEletrica = CompanhiaEletrica.Buscar(idResidencia);
                return Ok(companhiaEletrica);
            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao buscar a companhia elétrica: {ex.Message}");
            }
        }
    }
}
