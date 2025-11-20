using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Implementations
{
    public class PerformanceEvaluationRepository : GenericRepository<TPLPerformanceEvaluation>, IPerformanceEvaluationRepository
    {
        public PerformanceEvaluationRepository(HRSystemContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<TPLPerformanceEvaluation>> GetAllAsync()
        {
            return await _dbSet
                .Include(x => x.Criteria)
                .Include(x => x.Employee)
                .ToListAsync();
        }

        public override async Task<TPLPerformanceEvaluation?> GetByIdAsync(object id)
        {
            return await _dbSet
                .Include(x => x.Criteria)
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.EvaluationID == (int)id);
        }
    }
}

