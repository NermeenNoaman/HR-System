// In HRSystem.Infrastructure/Implementations/TPLProjectAssignmentRepository.cs
using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;
using HRSystem.Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TPLProjectAssignmentRepository : GenericRepository<TPLProject_Assignment>, ITPLProjectAssignmentRepository
{
    private readonly HRSystemContext _context;

    public TPLProjectAssignmentRepository(HRSystemContext context) : base(context)
    {
        _context = context;
    }

    // Logic: Check if the employee is already assigned to this specific project
    public async Task<bool> IsAssignedAsync(int employeeId, int projectId)
    {
        return await _context.Set<TPLProject_Assignment>()
            .AnyAsync(a => a.EmployeeID == employeeId && a.ProjectID == projectId);
    }

    // Reporting: Get all assignments for a specific project
    public async Task<IEnumerable<TPLProject_Assignment>> GetAssignmentsByProjectIdAsync(int projectId)
    {
        return await _context.Set<TPLProject_Assignment>()
            .Where(a => a.ProjectID == projectId)
            .ToListAsync();
    }
}