using HRSystem.BaseLibrary.Models;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    // Assume TPLTraining is the Entity name for the courses
    public interface ITPLEmployeeTrainingRepository : IGenericRepository<TPLEmployee_Training>
    {
        // Logic 1: Prevent duplicate enrollment
        Task<bool> IsEmployeeRegisteredAsync(int employeeId, int trainingId);
    }
}