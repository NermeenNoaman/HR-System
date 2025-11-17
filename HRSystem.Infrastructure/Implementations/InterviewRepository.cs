using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations
{
    public class InterviewRepository : GenericRepository<TPLInterview>, IInterviewRepository
    {
        public InterviewRepository(HRSystemContext context) : base(context)
        {
        }
    }
}


