// In HRSystem.Infrastructure/Implementations/TPLEmployeeTrainingRepository.cs
using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;
using HRSystem.Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class TPLEmployeeTrainingRepository : GenericRepository<TPLEmployee_Training>, ITPLEmployeeTrainingRepository
{
    private readonly HRSystemContext _context;

    public TPLEmployeeTrainingRepository(HRSystemContext context) : base(context)
    {
        _context = context;
    }

    // Logic 1: Check if the employee is already enrolled in the specific training
    public async Task<bool> IsEmployeeRegisteredAsync(int employeeId, int trainingId)
    {
        return await _context.Set<TPLEmployee_Training>()
            .AnyAsync(et => et.EmployeeID == employeeId && et.TrainingID == trainingId);
    }
}