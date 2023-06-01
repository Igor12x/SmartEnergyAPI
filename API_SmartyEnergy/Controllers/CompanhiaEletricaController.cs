using API_SmartyEnergy.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace API_SmartyEnergy.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CompanhiaEletricaController : ControllerBase {
        [HttpGet("buscarCompanhia/{id}")]
        public IActionResult BuscarCompanhia(int id) {
            try {
                CompanhiaEletrica companhiaEletrica = CompanhiaEletrica.Buscar(id);
                return Ok(companhiaEletrica);
            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao buscar a companhia elétrica: {ex.Message}");
            }
        }
    }
}
