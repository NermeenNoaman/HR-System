using HRSystem.BaseLibrary.DTOs;
using HRSystem.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class HRNeedRequestController : ControllerBase
    {
        private readonly IHRNeedRequestService _service;

        public HRNeedRequestController(IHRNeedRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "admin,HR")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<HRNeedRequestReadDto>))]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await _service.GetAllAsync();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,HR")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HRNeedRequestReadDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound(new { Message = $"HR Need Request with ID {id} not found." });
            }
            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "admin,HR")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(HRNeedRequestReadDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] HRNeedRequestCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdDto = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdDto.HRNeedID }, createdDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,HR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] HRNeedRequestUpdateDto dto)
        {
            if (id != dto.HRNeedID)
            {
                return BadRequest(new { Message = "ID mismatch between route and body." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.UpdateAsync(id, dto);
            if (!result)
            {
                return NotFound(new { Message = $"HR Need Request with ID {id} not found for update." });
            }

            return Ok(new { Message = "HR Need Request updated successfully." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new { Message = $"HR Need Request with ID {id} not found." });
            }

            return NoContent();
        }
}

