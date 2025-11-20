using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations
{
    public class EvaluationCriteriaRepository : GenericRepository<TPLEvaluationCriterion>, IEvaluationCriteriaRepository
    {
        public EvaluationCriteriaRepository(HRSystemContext context) : base(context)
        {
        }
    }
}


