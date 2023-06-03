using Microsoft.AspNetCore.Mvc;
using SmartAPI.Models;

namespace SmartAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class FaturaController : ControllerBase {
        /// <summary>
        /// Obtém a última fatura com base no ID da Residência
        /// </summary>
        /// <param name="idResidencia">ID da Residência</param>
        /// <returns>O valor e o consumo registrado na ultima fatura</returns>
        [HttpGet("UltimaFatura/{idResidencia}")]
        public IActionResult BuscarValorUltimaFatura(int idResidencia) {
            try {
                Fatura fatura = Fatura.BuscarValorUltimaFatura(idResidencia);
                return Ok(fatura);
            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao buscar a última fatura: {ex.Message}");
            }
        }
    }
}
