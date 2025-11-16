// In HRSystem.Infrastructure/Implementations/TPLLeaveBalanceRepository.cs
using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;
using HRSystem.Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class TPLLeaveBalanceRepository : GenericRepository<TPLLeaveBalance>, ITPLLeaveBalanceRepository
{
    private readonly HRSystemContext _context;

    public TPLLeaveBalanceRepository(HRSystemContext context) : base(context)
    {
        _context = context;
    }

    // Implementation of balance retrieval for validation
    public async Task<TPLLeaveBalance> GetBalanceForValidationAsync(int employeeId, int leaveTypeId, short year)
    {
        return await _context.Set<TPLLeaveBalance>()
            .FirstOrDefaultAsync(b =>
                b.EmployeeId == employeeId &&
                b.LeaveTypeId == leaveTypeId &&
                b.Year == year);
    }

    // Implementation of subtracting used days (Step 4)
    public async Task<bool> SubtractUsedDaysAsync(int employeeId, int leaveTypeId, short year, int daysToSubtract)
    {
        var balance = await GetBalanceForValidationAsync(employeeId, leaveTypeId, year);

        if (balance == null)
        {
            // Balance record not found for the current year/type
            return false;
        }

        // IMPORTANT: We trust the Service Layer to have checked for sufficient balance.
        // This Repository only performs the update.
        balance.UsedDays += daysToSubtract;

        // Optionally, check for negative balance defensively, though the Service should handle this.
        if (balance.AllocatedDays - balance.UsedDays < 0)
        {
            // This indicates a severe logic error in the service layer, but we prevent data corruption.
            // You might log this error here.
            return false;
        }

        await _context.SaveChangesAsync();

        return true;
    }
}
