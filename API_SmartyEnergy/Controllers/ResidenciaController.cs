using API_SmartyEnergy.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace API_SmartyEnergy.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ResidenciaController : ControllerBase {
        /// <summary>
        /// Lista as residências associadas a um cliente específico.
        /// </summary>
        /// <param name="idCliente">O ID do cliente.</param>
        /// <returns>Uma lista de objetos Residencia.</returns>
        [HttpGet("ListarResidencias/{idCliente}")]
        public IActionResult ListarResidencias(int idCliente) {
            try {
                List<Residencia> residencias = Residencia.Listar(idCliente);
                return Ok(residencias);
            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao buscar as residências: {ex.Message}");
            }
        }
    }
}
