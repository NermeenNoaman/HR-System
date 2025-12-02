using AutoMapper;
using HRSystem.BaseLibrary.DTOs;
using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem_Wizer_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SelfServiceRequestController : ControllerBase
    {
        private readonly IGenericRepository<TPLSelfServiceRequest> _repository;
        private readonly IMapper _mapper;

        public SelfServiceRequestController(IGenericRepository<TPLSelfServiceRequest> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/SelfServiceRequest
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelfServiceRequestReadDto>))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var entities = await _repository.GetAllAsync();
                var dtos = _mapper.Map<IEnumerable<SelfServiceRequestReadDto>>(entities);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GET: api/SelfServiceRequest/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SelfServiceRequestReadDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                {
                    return NotFound(new { Message = $"Self Service Request with ID {id} not found." });
                }

                var dto = _mapper.Map<SelfServiceRequestReadDto>(entity);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // POST: api/SelfServiceRequest
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SelfServiceRequestReadDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] SelfServiceRequestCreateDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var entity = _mapper.Map<TPLSelfServiceRequest>(dto);
                var createdEntity = await _repository.AddAsync(entity);
                await _repository.SaveChangesAsync();

                var createdDto = _mapper.Map<SelfServiceRequestReadDto>(createdEntity);
                return CreatedAtAction(nameof(GetById), new { id = createdDto.RequestID }, createdDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // PUT: api/SelfServiceRequest/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] SelfServiceRequestUpdateDto dto)
        {
            try
            {
                if (id != dto.RequestID)
                {
                    return BadRequest(new { Message = "ID mismatch between route and body." });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingEntity = await _repository.GetByIdAsync(id);
                if (existingEntity == null)
                {
                    return NotFound(new { Message = $"Self Service Request with ID {id} not found." });
                }

                _mapper.Map(dto, existingEntity);
                await _repository.UpdateAsync(existingEntity);
                await _repository.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // DELETE: api/SelfServiceRequest/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                {
                    return NotFound(new { Message = $"Self Service Request with ID {id} not found." });
                }

                await _repository.DeleteAsync(entity);
                await _repository.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
