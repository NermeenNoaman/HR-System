using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations.Repositories
{
    public class SalaryRepository : GenericRepository<LKPSalary>, ISalaryRepository
    {
        public SalaryRepository(HRSystemContext context) : base(context) { }
    }
}
