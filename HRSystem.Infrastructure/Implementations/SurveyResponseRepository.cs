using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Implementations
{
    public class SurveyResponseRepository : GenericRepository<TPLSurvey_Response>, ISurveyResponseRepository
    {
        public SurveyResponseRepository(HRSystemContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<TPLSurvey_Response>> GetAllAsync()
        {
            return await _dbSet
                .Include(x => x.Survey)
                .Include(x => x.Employee)
                .ToListAsync();
        }

        public override async Task<TPLSurvey_Response?> GetByIdAsync(object id)
        {
            return await _dbSet
                .Include(x => x.Survey)
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.ResponseID == (int)id);
        }
    }
}

