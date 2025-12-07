
using AutoMapper;
using HRSystem.BaseLibrary.DTOs;
using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceRepository _attendanceRepo;
    private readonly IMapper _mapper;
    private readonly ITPLEmployeeRepository _employeeRepo;
    private readonly IUserRepository _userRepo;

    public AttendanceController(IAttendanceRepository attendanceRepo, IMapper mapper, ITPLEmployeeRepository employeeRepo, IUserRepository userRepo)
    {
        _attendanceRepo = attendanceRepo;
        _mapper = mapper;
        _employeeRepo = employeeRepo;
        _userRepo = userRepo;
    }

    private int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (int.TryParse(userIdClaim, out int userId)) return userId;
        throw new UnauthorizedAccessException("User ID not found in token.");
    }

    // ----------------------------------------------------------------------
    // 1. POST: Check-In (Final Simplified Logic - No Request Body)
    // ----------------------------------------------------------------------
    [HttpPost("checkin")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AttendanceReadDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    // 🟢 STEP 1: Eliminate [FromBody] DTO - The request has no body.
    public async Task<IActionResult> CheckIn()
    {
        try
        {
            // 1. Get Security/Time Data
            var currentUserId = GetCurrentUserId();
            var currentDate = DateTime.Now.Date;

            // 2. Security Check & ID Mapping: Map User ID (from token) to actual Employee ID
            var userRecord = await _userRepo.GetByIdAsync(currentUserId);
            if (userRecord == null)
            {
                return NotFound(new { Message = $"Check-in failed. User ID {currentUserId} does not exist in the user database." });
            }
            var actualEmployeeId = userRecord.EmployeeID;

            // 3. Prevent Duplicates: Check for an active Check-In today
            var existingRecord = await _attendanceRepo.GetTodayAttendanceRecordAsync(actualEmployeeId, currentDate);
            if (existingRecord != null)
            {
                return BadRequest(new { Message = "You have already checked in today." });
            }

            // 4. Create and Record Entity
            // 🟢 STEP 2: Manually create the Entity without using AutoMapper on an input DTO
            var entity = new TPLAttendance
            {
                EmployeeID = actualEmployeeId,
                Date = currentDate,
                CheckIn = DateTime.Now.TimeOfDay, // Record current time only
                Status = "Present",
                CheckOut = null                   // Must be NULL for an active record
            };

            var createdEntity = await _attendanceRepo.AddAsync(entity);
            var createdDto = _mapper.Map<AttendanceReadDto>(createdEntity);

            return CreatedAtAction(nameof(GetAttendanceById), new { id = createdDto.AttendanceID }, createdDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Check-in failed: " + ex.Message });
        }
    }

    // ----------------------------------------------------------------------
    // 2. POST: Check-Out (No Request Body needed)
    // ----------------------------------------------------------------------
    [HttpPost("checkout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CheckOut()
    {
        // 1. Get Security/Time Data
        int currentUserId = GetCurrentUserId();
        var currentDate = DateTime.Now.Date;
        var currentTimeSpan = DateTime.Now.TimeOfDay;

        // 2. Security Check & ID Mapping
        var userRecord = await _userRepo.GetByIdAsync(currentUserId);
        if (userRecord == null)
        {
            return NotFound(new { Message = $"Check-out failed. User ID {currentUserId} not found in user records." });
        }
        int actualEmployeeId = userRecord.EmployeeID;

        // 3. Search for the active attendance record
        var existingRecord = await _attendanceRepo.GetTodayAttendanceRecordAsync(actualEmployeeId, currentDate);

        if (existingRecord == null)
        {
            return NotFound(new { Message = "No active check-in record found for today." });
        }

        // 4. Record Check-Out Time
        bool success = await _attendanceRepo.RecordCheckOutAsync(existingRecord.AttendanceID, currentTimeSpan);

        if (success)
        {
            return Ok(new { Message = "Check-out recorded successfully." });
        }
        return BadRequest(new { Message = "Check-out failed." });
    }

    // ----------------------------------------------------------------------
    // 3. GET: Get All Attendance Records (for HR/Admin view)
    // ----------------------------------------------------------------------
    [HttpGet]
    [Route("all")] // 👈🏻 New route: /api/Attendance/all
    [Authorize(Roles = "HR,admin")] // 👈🏻 Security: Only HR or Admin can view all records
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AttendanceReadDto>))]
    public async Task<IActionResult> GetAllAttendance()
    {
        try
        {
            // Get all entities using the generic repository method
            var entities = await _attendanceRepo.GetAllAsync();

            if (entities == null || !entities.Any())
            {
                return NotFound(new { Message = "No attendance records found." });
            }

            // Map the list of entities to the list of DTOs for client consumption
            var dtoList = _mapper.Map<IEnumerable<AttendanceReadDto>>(entities);

            return Ok(dtoList);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to retrieve attendance records: " + ex.Message });
        }
    }

    // ----------------------------------------------------------------------
    // 4. GET: Get Attendance Record by ID (for HR/Admin view)
    // ----------------------------------------------------------------------
    [HttpGet("{id}")]
    [Authorize(Roles = "HR,admin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AttendanceReadDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAttendanceById(int id)
    {
        var entity = await _attendanceRepo.GetByIdAsync(id);
        if (entity == null) return NotFound();
        var dto = _mapper.Map<AttendanceReadDto>(entity);
        return Ok(dto);
    }
    private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double EarthRadiusMeters = 6371000d;
        double dLat = DegreesToRadians(lat2 - lat1);
        double dLon = DegreesToRadians(lon2 - lon1);

        double a = Math.Pow(Math.Sin(dLat / 2), 2) +
                   Math.Cos(DegreesToRadians(lat1)) *
                   Math.Cos(DegreesToRadians(lat2)) *
                   Math.Pow(Math.Sin(dLon / 2), 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return EarthRadiusMeters * c;
    }

    private static double DegreesToRadians(double angle)
        => angle * (Math.PI / 180d);
}
