using API_SmartyEnergy.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace API_SmartyEnergy.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ResidenciaController : ControllerBase {
        [HttpGet("listarResidencias/{id}")]
        public IActionResult ListarResidencias(int id) {
            try {
                List<Residencia> residencias = Residencia.Listar(id);
                return Ok(residencias);
            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao buscar as residências: {ex.Message}");
            }
        }
    }
}
