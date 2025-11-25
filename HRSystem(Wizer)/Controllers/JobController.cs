using HRSystem.BaseLibrary.DTOs;
using HRSystem.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class JobController : ControllerBase
    {
    private readonly IJobService _service;

    public JobController(IJobService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles="admin,HR")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<JobReadDto>))]
    public async Task<IActionResult> GetAll()
    {
        var dtos = await _service.GetAllAsync();
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    [Authorize(Roles="admin,HR")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JobReadDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var dto = await _service.GetByIdAsync(id);
        if (dto == null)
        {
            return NotFound(new { Message = $"Job with ID {id} not found." });
        }
        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles="admin,HR")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(JobReadDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] JobCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdDto = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdDto.JobID }, createdDto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin,HR")] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] JobUpdateDto dto)
    {
        if (id != dto.JobID)
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
            return NotFound(new { Message = $"Job with ID {id} not found for update." });
        }

        return Ok(new { Message = "Job updated successfully." });
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
            return NotFound(new { Message = $"Job with ID {id} not found." });
        }

        return NoContent();
    }
}

