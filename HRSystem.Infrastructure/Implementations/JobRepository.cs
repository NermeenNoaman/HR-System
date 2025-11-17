using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations
{
    public class JobRepository : GenericRepository<TPLJob>, IJobRepository
    {
        public JobRepository(HRSystemContext context) : base(context)
        {
        }
    }
}


