using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartEnergyAPI.Models;

namespace SmartEnergyAPI.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase {
        [HttpPost]
        public IActionResult Login([FromBody] Login login) {

            Cliente cliente = (Cliente)login.validarLogin(login);
            string json = JsonConvert.SerializeObject(cliente); 
            return Ok(json); 
        }

    }
}
