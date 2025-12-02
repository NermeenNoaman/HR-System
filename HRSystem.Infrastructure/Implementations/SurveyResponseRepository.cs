using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations.Repositories
{
    public class SurveyResponseRepository : GenericRepository<TPLSurvey_Response>, ISurveyResponseRepository
    {
        public SurveyResponseRepository(HRSystemContext context) : base(context) { }
    }
}
