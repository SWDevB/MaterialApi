using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaterialApi.Services;
using MaterialApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaterialApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialService _materialService;
        private readonly ILogger<MaterialsController> _logger;

        public MaterialsController(IMaterialService materialService, ILogger<MaterialsController> logger)
        {
            _materialService = materialService;
            _logger = logger;
        }

        // GET: api/<MaterialsController>
        [HttpGet]
        public IEnumerable<Material> Get([FromQuery] string name)
        {
            return _materialService.Get(name);
        }

        // GET api/<MaterialsController>/5
        [HttpGet("{id}")]
        public Material GetById(string id)
        {
            return _materialService.GetById(id);
        }

        // POST api/<MaterialsController>
        [HttpPost]
        public void Post([FromBody] Material material)
        {
            _materialService.Add(material);
        }

        // PUT api/<MaterialsController>/5
        [HttpPut]
        public void Put([FromBody] Material material)
        {
            _materialService.Save(material);
        }

        // DELETE api/<MaterialsController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _materialService.Delete(id);
        }
    }
}
