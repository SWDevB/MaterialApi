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
        public ActionResult<Material> GetById(string id)
        {
            var result = _materialService.GetById(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        // POST api/<MaterialsController>
        [HttpPost]
        public ActionResult<Material> Post([FromBody] Material material)
        {
            _materialService.Add(material);
            //TODO: create url more reliable
            var url = string.Concat(this.Request.Scheme, "://", this.Request.Host, this.Request.Path, $"/{material.Id}");
            return Created(url, material);
        }

        // PUT api/<MaterialsController>/5
        [HttpPut]
        public ActionResult Put([FromBody] Material material)
        {
            if (_materialService.Save(material))
                return Ok();
            else
                return NotFound();
        }

        // DELETE api/<MaterialsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            if (_materialService.Delete(id))
                return Ok();
            else
                return NotFound();
        }
    }
}
