// In HRSystem(Wizer)/Controllers/EmployeeController.cs

using AutoMapper;
using HRSystem.BaseLibrary.DTOs;
using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]

public class EmployeeController : ControllerBase
{
    private readonly ITPLEmployeeRepository _employeeRepo;
    private readonly IMapper _mapper;

    
    public EmployeeController(ITPLEmployeeRepository employeeRepo, IMapper mapper)
    {
        _employeeRepo = employeeRepo;
        _mapper = mapper;
    }

    // ----------------------------------------------------------------------
    // 1. POST: Create New Employee (HR Action)
    // ----------------------------------------------------------------------
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EmployeeReadDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDto dto)
    {
        
        var entity = _mapper.Map<TPLEmployee>(dto);

        
        var createdEntity = await _employeeRepo.AddAsync(entity);
        await _employeeRepo.SaveChangesAsync();


        var createdDto = _mapper.Map<EmployeeReadDto>(createdEntity);

        
        return CreatedAtAction(nameof(GetEmployee), new { id = createdDto.EmployeeId }, createdDto);
    }

    // ----------------------------------------------------------------------
    // 2. GET: Get Employee by ID
    // ----------------------------------------------------------------------
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeReadDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployee(int id)
    {
        
        var entity = await _employeeRepo.GetEmployeeContactInfoAsync(id);
        if (entity == null)
        {
            return NotFound(new { Message = $"Employee with ID {id} not found." });
        }

        var dto = _mapper.Map<EmployeeReadDto>(entity);
        return Ok(dto);
    }

    // ----------------------------------------------------------------------
    // 3. PUT: Update Existing Employee Details
    // ----------------------------------------------------------------------
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeUpdateDto dto)
    {
        if (id != dto.EmployeeId)
        {
            return BadRequest(new { Message = "ID mismatch between route and body." });
        }

        var existingEntity = await _employeeRepo.GetByIdAsync(id);
        if (existingEntity == null)
        {
            return NotFound(new { Message = $"Employee with ID {id} not found for update." });
        }

        
        _mapper.Map(dto, existingEntity);

        
        await _employeeRepo.UpdateAsync(existingEntity);
        await _employeeRepo.SaveChangesAsync();


        return NoContent(); // 204 No Content for successful update
    }

    // ----------------------------------------------------------------------
    // 4. DELETE: Remove Employee (HR Action)
    // ----------------------------------------------------------------------
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var entity = await _employeeRepo.GetByIdAsync(id);
        if (entity == null)
        {
            return NotFound(new { Message = $"Employee with ID {id} not found." });
        }

        await _employeeRepo.DeleteAsync(entity);
        await _employeeRepo.SaveChangesAsync();


        return NoContent();
    }
}