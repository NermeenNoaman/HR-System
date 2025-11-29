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
public class TrainingController : ControllerBase
{
    private readonly ITPLTrainingRepository _trainingRepo;
    private readonly IMapper _mapper;

    public TrainingController(ITPLTrainingRepository trainingRepo, IMapper mapper)
    {
        _trainingRepo = trainingRepo;
        _mapper = mapper;
    }

    // =========================================================================
    // POST: Create New Training Course
    // =========================================================================
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TPLTrainingReadDTO))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateTraining([FromBody] TPLTrainingCreateDTO dto)
    {
        // Validation: Check if training title already exists
        var existing = await _trainingRepo.GetByTitleAsync(dto.Title);
        if (existing != null)
        {
            return Conflict(new { Message = $"Training title '{dto.Title}' already exists." });
        }

        var entity = _mapper.Map<TPLTraining>(dto);
        var createdEntity = await _trainingRepo.AddAsync(entity);
        await _trainingRepo.SaveChangesAsync();

        var createdDto = _mapper.Map<TPLTrainingReadDTO>(createdEntity);
        return CreatedAtAction(nameof(GetTrainingById), new { id = createdDto.TrainingID }, createdDto);
    }

    // =========================================================================
    // GET: Get All Trainings
    // =========================================================================
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TPLTrainingReadDTO>))]
    public async Task<IActionResult> GetAllTrainings()
    {
        var entities = await _trainingRepo.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<TPLTrainingReadDTO>>(entities);
        return Ok(dtos);
    }

    // =========================================================================
    // GET: Get Training by ID
    // =========================================================================
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TPLTrainingReadDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTrainingById(int id)
    {
        var entity = await _trainingRepo.GetByIdAsync(id);
        if (entity == null)
        {
            return NotFound(new { Message = $"Training with ID {id} not found." });
        }
        var dto = _mapper.Map<TPLTrainingReadDTO>(entity);
        return Ok(dto);
    }

    // =========================================================================
    // PUT: Update Training Course
    // =========================================================================
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TPLTrainingReadDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateTraining(int id, [FromBody] TPLTrainingUpdateDTO dto)
    {
        var entityToUpdate = await _trainingRepo.GetByIdAsync(id);
        if (entityToUpdate == null)
        {
            return NotFound(new { Message = $"Training with ID {id} not found." });
        }

        // Validation: Check if the new title already exists for a *different* training
        if (!string.IsNullOrWhiteSpace(dto.Title) && dto.Title != entityToUpdate.Title)
        {
            var existing = await _trainingRepo.GetByTitleAsync(dto.Title);
            if (existing != null && existing.TrainingID != id)
            {
                return Conflict(new { Message = $"Training title '{dto.Title}' already exists for another training." });
            }
        }

        // Use AutoMapper to apply changes from DTO to the retrieved entity
        _mapper.Map(dto, entityToUpdate);

        await _trainingRepo.UpdateAsync(entityToUpdate);
        await _trainingRepo.SaveChangesAsync();

        var updatedDto = _mapper.Map<TPLTrainingReadDTO>(entityToUpdate);
        return Ok(updatedDto);
    }

    // =========================================================================
    // DELETE: Delete Training Course
    // =========================================================================
    [HttpDelete("{id}")]
    [Authorize(Roles ="admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTraining(int id)
    {
        var entityToDelete = await _trainingRepo.GetByIdAsync(id);
        if (entityToDelete == null)
        {
            return NotFound(new { Message = $"Training with ID {id} not found." });
        }

        // Note: Delete may fail if there are foreign key constraints (Employee_Training records exist)
        await _trainingRepo.DeleteAsync(entityToDelete);
        await _trainingRepo.SaveChangesAsync();

        return NoContent(); // 204 No Content indicates successful deletion
    }
}