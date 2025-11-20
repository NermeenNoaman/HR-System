using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Implementations
{
    public class BenefitsCompensationRepository : GenericRepository<TPLBenefitsCompensation>, IBenefitsCompensationRepository
    {
        public BenefitsCompensationRepository(HRSystemContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<TPLBenefitsCompensation>> GetAllAsync()
        {
            return await _dbSet
                .Include(x => x.BenefitType)
                .Include(x => x.Employee)
                .ToListAsync();
        }

        public override async Task<TPLBenefitsCompensation?> GetByIdAsync(object id)
        {
            return await _dbSet
                .Include(x => x.BenefitType)
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.BenefitID == (int)id);
        }
    }
}

