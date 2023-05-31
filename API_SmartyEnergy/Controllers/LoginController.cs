using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.WebApi.Jwt;
using Newtonsoft.Json;
using SmartEnergyAPI.Models;

namespace SmartEnergyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        public IActionResult Login([FromBody] Login loginCliente)
        {
            try
            {
                Cliente cliente = loginCliente.ValidarLogin(loginCliente);
                string json = JsonConvert.SerializeObject(cliente);
                return Ok(json);
            }
            catch (InvalidCredentialsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
