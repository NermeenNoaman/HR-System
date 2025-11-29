using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations.Repositories
{
    public class JobApplicationRepository : GenericRepository<LkpJobApplication>, IJobApplicationRepository
    {
        public JobApplicationRepository(HRSystemContext context) : base(context) { }
    }
}
