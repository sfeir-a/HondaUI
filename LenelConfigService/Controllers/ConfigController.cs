using LenelConfigService.Models;
using LenelConfigService.Services;
using Microsoft.AspNetCore.Mvc;

/*
    Routes:
    GET /api/config
    GET /api/config/{endpointName}
    POST /api/config
    PUT /api/config/{endpointName}
    DELETE /api/config/{endpointName}
*/
namespace LenelConfigService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigService _service;
        public ConfigController(IConfigService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Configuration>>> GetAll() =>
          Ok(await _service.GetAllAsync());

        [HttpGet("{endpointName}")]
        public async Task<ActionResult<Configuration>> Get(string endpointName)
        {
            var row = await _service.GetAsync(endpointName);
            return row is null ? NotFound() : Ok(row);
        }

        [HttpPost]
        public async Task<ActionResult<Configuration>> Create([FromBody] Configuration config)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = await _service.CreateAsync(config);
            return CreatedAtAction(nameof(Get), new { endpointName = created.EndpointName }, created);
        }

        [HttpPut("{endpointName}")]
        public async Task<IActionResult> Update(string endpointName, [FromBody] Configuration config)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var ok = await _service.UpdateAsync(endpointName, config);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{endpointName}")]
        public async Task<IActionResult> Delete(string endpointName)
        {
            var ok = await _service.DeleteAsync(endpointName);
            return ok ? NoContent() : NotFound();
        }

        [HttpGet("fields")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllFields()
        {
            var fields = await _service.GetAllFieldNamesAsync();
            return Ok(fields);
        }
    }
}
