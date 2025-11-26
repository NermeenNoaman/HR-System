using HRSystem.BaseLibrary.Models;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface ITPLProjectAssignmentRepository : IGenericRepository<TPLProject_Assignment>
    {
        // Logic: Check if the employee is already assigned to this project
        Task<bool> IsAssignedAsync(int employeeId, int projectId);

        // Reporting: Get all assignments for a specific project
        Task<IEnumerable<TPLProject_Assignment>> GetAssignmentsByProjectIdAsync(int projectId);
    }
}