using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAPI.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class FaturaController : ControllerBase {

        [HttpGet("UltimaFatura/{id}")]
        public IActionResult buscarValorUltimaFatura(int id)
        {
            string json = JsonConvert.SerializeObject(Fatura.buscarValorUltimaFatura(id));
            return Ok(json);

        }
    }
}
