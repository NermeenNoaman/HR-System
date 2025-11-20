using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations
{
    public class SurveyRepository : GenericRepository<TPLSurvey>, ISurveyRepository
    {
        public SurveyRepository(HRSystemContext context) : base(context)
        {
        }
    }
}


