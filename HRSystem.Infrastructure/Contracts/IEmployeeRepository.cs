// In HRSystem.Infrastructure/Contracts/ITPLEmployeeRepository.cs
using HRSystem.BaseLibrary.Models;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    // Inherits from IGenericRepository for general Employee CRUD
    public interface ITPLEmployeeRepository : IGenericRepository<TPLEmployee>
    {
        // Logic for retrieving contact details required for notifications/emails
        Task<TPLEmployee> GetEmployeeContactInfoAsync(int employeeId);

        // You may need a function to get the Manager's email based on the Employee (Requires ManagerID column/relationship in TPLEmployee)
        // Example: Task<string> GetManagerEmailByEmployeeIdAsync(int employeeId);
    }
}