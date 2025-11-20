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
        private readonly IEncryptService _encrypt;
        private readonly IMappingService _mapper;

        public ConfigController(IConfigService service, IEncryptService encrypt, IMappingService mapper)
        {
            _service = service;
            _encrypt = encrypt;
            _mapper = mapper;
        }

        // GET /api/config
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExtractConfigurationDto>>> GetAll()
        {
            var entities = await _service.GetAllAsync();
            var dtos = entities.Select(e => _mapper.ToDto(e));
            return Ok(dtos);
        }

        // GET /api/config/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExtractConfigurationDto>> Get(int id)
        {
            var entity = await _service.GetAsync(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.ToDto(entity));
        }

        // POST /api/config
        [HttpPost]
        public async Task<ActionResult<ExtractConfigurationDto>> Create([FromBody] ExtractConfigurationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map DTO → Entity
            var entity = _mapper.ToEntity(dto);

            // Encrypt password if provided
            if (!string.IsNullOrWhiteSpace(dto.CredentialPassword))
            {
                entity.CredentialPassword = _encrypt.Encrypt(dto.CredentialPassword);
            }

            // Save to DB
            var created = await _service.CreateAsync(entity);

            // Return mapped DTO
            return CreatedAtAction(nameof(Get),
                new { id = created.Id },
                _mapper.ToDto(created));
        }

        // PUT /api/config/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExtractConfigurationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _service.GetAsync(id);
            if (existing == null)
                return NotFound();

            // Map non-password fields
            _mapper.UpdateEntityFromDto(existing, dto);

            // Only update password when provided
            if (!string.IsNullOrWhiteSpace(dto.CredentialPassword))
            {
                existing.CredentialPassword = _encrypt.Encrypt(dto.CredentialPassword);
            }

            var ok = await _service.UpdateAsync(id, existing);
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
