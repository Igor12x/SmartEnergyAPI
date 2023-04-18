﻿using API_SmartyEnergy.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_SmartyEnergy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanhiaEletricaController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet ("buscarCompanhia/{id}")]
        public IActionResult buscarCompanhia(int id)
        {
            string json = JsonConvert.SerializeObject(CompanhiaEletrica.buscar(id));
            return Ok(json);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}