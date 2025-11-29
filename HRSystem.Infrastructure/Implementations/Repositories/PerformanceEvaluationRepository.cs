using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations.Repositories
{
    public class PerformanceEvaluationRepository : GenericRepository<TPLPerformanceEvaluation>, IPerformanceEvaluationRepository
    {
        public PerformanceEvaluationRepository(HRSystemContext context) : base(context) { }
    }
}
