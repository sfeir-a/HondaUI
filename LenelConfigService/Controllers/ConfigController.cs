using LenelConfigService.Models;
using LenelConfigService.Services;
using Microsoft.AspNetCore.Mvc;

/*
    Routes:
    GET    /api/config
    GET    /api/config/{id}
    POST   /api/config
    PUT    /api/config/{id}
    DELETE /api/config/{id}
    GET    /api/config/fields
*/
namespace LenelConfigService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigService _service;
        public ConfigController(IConfigService service) => _service = service;

        // GET /api/config
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExtractConfiguration>>> GetAll() =>
            Ok(await _service.GetAllAsync());

        // GET /api/config/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExtractConfiguration>> Get(int id)
        {
            var row = await _service.GetAsync(id);
            return row is null ? NotFound() : Ok(row);
        }

        // POST /api/config
        [HttpPost]
        public async Task<ActionResult<ExtractConfiguration>> Create([FromBody] ExtractConfiguration config)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(config);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT /api/config/{id}/deactivate
        [HttpPut("{id:int}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var ok = await _service.DeactivateAsync(id);
            return ok ? NoContent() : NotFound();
        }

        // PUT /api/config/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExtractConfiguration config)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ok = await _service.UpdateAsync(id, config);
            return ok ? NoContent() : NotFound();
        }

        // DELETE /api/config/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }

        // GET /api/config/fields
        [HttpGet("fields")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllFields()
        {
            var fields = await _service.GetAllFieldNamesAsync();
            return Ok(fields);
        }
    }
}