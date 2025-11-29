using AutoMapper;
using HRSystem.BaseLibrary.DTOs;
using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "HR,admin")]
public class EmployeeTrainingController : ControllerBase
{
    private readonly ITPLEmployeeTrainingRepository _empTrainRepo;
    private readonly IMapper _mapper;

    public EmployeeTrainingController(ITPLEmployeeTrainingRepository empTrainRepo, IMapper mapper)
    {
        _empTrainRepo = empTrainRepo;
        _mapper = mapper;
    }

    // =========================================================================
    // POST: Enroll Employee in a Training Course (Create)
    // =========================================================================
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TPLEmployeeTrainingReadDTO))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> EnrollEmployee([FromBody] TPLEmployeeTrainingCreateDTO dto)
    {
        // Logic 1: Prevent duplicate enrollment
        bool alreadyRegistered = await _empTrainRepo.IsEmployeeRegisteredAsync(dto.EmployeeID, dto.TrainingID);
        if (alreadyRegistered)
        {
            return Conflict(new { Message = $"Employee {dto.EmployeeID} is already registered for Training {dto.TrainingID}." });
        }

        var entity = _mapper.Map<TPLEmployee_Training>(dto);
        var createdEntity = await _empTrainRepo.AddAsync(entity);
        await _empTrainRepo.SaveChangesAsync();

        var createdDto = _mapper.Map<TPLEmployeeTrainingReadDTO>(createdEntity);
        // Note: Using GetEmployeeTrainingRecord for CreatedAtAction path, which is not ideal 
        // as the key is composite (EmployeeID, TrainingID). 
        // We'll use GetEmployeeTrainingRecord as the closest available path.
        return CreatedAtAction(nameof(GetEmployeeTrainingRecord), new { employeeId = createdDto.EmployeeID }, createdDto);
    }

    // =========================================================================
    // GET: Get All Training Records (Admin/HR View)
    // =========================================================================
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TPLEmployeeTrainingReadDTO>))]
    public async Task<IActionResult> GetAllEmployeeTrainingRecords()
    {
        var entities = await _empTrainRepo.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<TPLEmployeeTrainingReadDTO>>(entities);
        return Ok(dtos);
    }


    // =========================================================================
    // GET: Get Training Records for a Specific Employee (Read Filtered)
    // =========================================================================
    [HttpGet("employee/{employeeId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TPLEmployeeTrainingReadDTO>))]
    public async Task<IActionResult> GetEmployeeTrainingRecord(int employeeId)
    {
        var entities = await _empTrainRepo.FindAsync(et => et.EmployeeID == employeeId);
        var dtos = _mapper.Map<IEnumerable<TPLEmployeeTrainingReadDTO>>(entities);

        if (!entities.Any()) // Check if the list is empty
        {
            return NotFound(new { Message = $"No training records found for Employee ID {employeeId}." });
        }

        return Ok(dtos);
    }

    // =========================================================================
    // PUT: Update Completion Status and Score (Update)
    // Note: Since the PK is composite, we must pass both IDs in the route.
    // =========================================================================
    [HttpPut("employee/{employeeId}/training/{trainingId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEmployeeTraining(int employeeId, int trainingId, [FromBody] TPLEmployeeTrainingUpdateDTO dto)
    {
        // Get the existing entity using the composite key
        var existingEntity = await _empTrainRepo.GetByIdAsync(new { EmployeeID = employeeId, TrainingID = trainingId });

        if (existingEntity == null)
        {
            return NotFound(new { Message = $"Enrollment record not found for Employee {employeeId} in Training {trainingId}." });
        }

        // Apply updates (Score, CompletionStatus)
        _mapper.Map(dto, existingEntity);

        await _empTrainRepo.UpdateAsync(existingEntity);
        await _empTrainRepo.SaveChangesAsync();

        return NoContent();
    }

    // =========================================================================
    // DELETE: Remove Enrollment Record (Delete)
    // =========================================================================
    [HttpDelete("employee/{employeeId}/training/{trainingId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEmployeeTraining(int employeeId, int trainingId)
    {
        // Get the entity using the composite key
        var entityToDelete = await _empTrainRepo.GetByIdAsync(new { EmployeeID = employeeId, TrainingID = trainingId });

        if (entityToDelete == null)
        {
            return NotFound(new { Message = $"Enrollment record not found for Employee {employeeId} in Training {trainingId}." });
        }

        // Delete the record
        await _empTrainRepo.DeleteAsync(entityToDelete);
        await _empTrainRepo.SaveChangesAsync();

        return NoContent();
    }
}