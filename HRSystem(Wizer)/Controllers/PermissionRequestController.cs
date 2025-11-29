// In HRSystem(Wizer)/Controllers/PermissionRequestController.cs

using AutoMapper;
using HRSystem.BaseLibrary.DTOs;
using HRSystem.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
[Authorize] 
public class PermissionRequestController : ControllerBase
{
    private readonly IPermissionManagementService _permissionService;
    private readonly IMapper _mapper;

    public PermissionRequestController(IPermissionManagementService permissionService, IMapper mapper)
    {
        _permissionService = permissionService;
        _mapper = mapper;
    }

    // Helper to get the current User ID from the JWT Token
    private int GetCurrentUserId()
    {
        // 1. البحث عن الـ Claim المخصص "EmployeeID"
        var employeeIdClaim = User.FindFirst("EmployeeID")?.Value;

        // 2. محاولة تحويله إلى رقم صحيح
        if (int.TryParse(employeeIdClaim, out int employeeId))
        {
            // هذا يضمن أننا نستخدم 202410 بدلاً من 107
            return employeeId;
        }

        // 3. التعامل مع حالة الـ ID غير الصالح أو المفقود
        throw new UnauthorizedAccessException("Employee ID claim is missing or invalid in the token.");
    }

    // ----------------------------------------------------------------------
    // 1. POST: Submit New Permission Request (Employee Action)
    // (Calls ProcessNewPermissionRequestAsync which handles auto-check)
    // ----------------------------------------------------------------------
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermissionReadDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SubmitPermissionRequest([FromBody] PermissionCreateDto dto)
    {
        try
        {
            // Use Employee ID from the token for security
            dto.EmployeeId = GetCurrentUserId();

            var result = await _permissionService.ProcessNewPermissionRequestAsync(dto);

            // If auto-rejected due to exceeding limits, return error
            if (result.Status.StartsWith("AutoRejected"))
            {
                return BadRequest(new { Message = "Request automatically rejected: " + result.Status, Status = result.Status });
            }

            // Status is Pending - Manager Notification Logic executed in Service
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message }); // Handles overlap checks
        }
    }

    // ----------------------------------------------------------------------
    // 2. GET: Get Request Details
    // ----------------------------------------------------------------------
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermissionReadDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPermissionRequest(int id)
    {
        var result = await _permissionService.GetPermissionRequestByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    // ----------------------------------------------------------------------
    // 3. POST: Approve Request (Manager Action)
    // ----------------------------------------------------------------------
    [HttpPost("approve/{permissionId}")]
    [Authorize(Roles = "admin,HR")] // 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ApproveRequest(int permissionId)
    {
        int managerId = GetCurrentUserId();

        bool success = await _permissionService.ApprovePermissionRequestAsync(permissionId, managerId);

        if (success)
        {
            return Ok(new { Message = "Permission request approved successfully." });
        }

        // This handles cases where status is not 'Pending' or request is not found
        return BadRequest(new { Message = "Could not approve request. It may already be processed or not exist." });
    }

    // ----------------------------------------------------------------------
    // 4. POST: Reject Request (Manager Action)
    // ----------------------------------------------------------------------
    [HttpPost("reject/{permissionId}")]
    [Authorize(Roles = "admin,HR")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RejectRequest(int permissionId)
    {
        int managerId = GetCurrentUserId();

        bool success = await _permissionService.RejectPermissionRequestAsync(permissionId, managerId);

        if (success)
        {
            return Ok(new { Message = "Permission request rejected successfully." });
        }

        return BadRequest(new { Message = "Could not reject request. It may already be processed or not exist." });
    }
}