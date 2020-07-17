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
        [ProducesResponseType(typeof(IEnumerable<Material>), 200)]
        [ProducesResponseType(500)]
        public ActionResult<IEnumerable<Material>> Get([FromQuery] string name)
        {
            try
            {
                return Ok(_materialService.Get(name));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occured on GET (name: {name}");
                return Problem("Error Occured");
            }
        }

        // GET api/<MaterialsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Material), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<Material> GetById(string id)
        {
            try
            {
                var result = _materialService.GetById(id);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occured on GET (id: {id}");
                return Problem("Error Occured");
            }
        }

        // POST api/<MaterialsController>
        [HttpPost]
        [ProducesResponseType(typeof(Material), 200)]
        [ProducesResponseType(500)]
        public ActionResult<Material> Post([FromBody] Material material)
        {
            try
            {
                _materialService.Add(material);
                return Ok(material);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occured on POST");
                return Problem("Error Occured");
            }
        }

        // PUT api/<MaterialsController>/5
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult Put([FromBody] Material material)
        {
            try
            {
                if (_materialService.Update(material))
                    return Ok();
                else
                    return NotFound();
            }
            catch (ArgumentException aex)
            {
                _logger.LogError(aex, $"ArgumentException occured on PUT");
                return BadRequest(aex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occured on PUT");
                return Problem("Error Occured");
            }
        }

        // DELETE api/<MaterialsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult Delete(string id)
        {
            try
            {
                if (_materialService.Delete(id))
                return Ok();
            else
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occured on DELETE");
                return Problem("Error Occured");
            }
        }
    }
}
