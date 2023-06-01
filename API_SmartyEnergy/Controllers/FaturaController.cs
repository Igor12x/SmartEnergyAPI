using Microsoft.AspNetCore.Mvc;
using SmartAPI.Models;

namespace SmartAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class FaturaController : ControllerBase {
        [HttpGet("UltimaFatura/{id}")]
        public IActionResult BuscarValorUltimaFatura(int id) {
            try {
                Fatura fatura = Fatura.BuscarValorUltimaFatura(id);
                return Ok(fatura);
            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao buscar a última fatura: {ex.Message}");
            }
        }
    }
}
