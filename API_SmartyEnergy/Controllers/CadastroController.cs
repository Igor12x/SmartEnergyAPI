using API_SmartEnergy.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_SmartEnergy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase {
        [HttpPost]
        public void Post([FromBody] Cliente cliente) { 
            Cadastro.Cadastrar(cliente);
    }
    }
}
