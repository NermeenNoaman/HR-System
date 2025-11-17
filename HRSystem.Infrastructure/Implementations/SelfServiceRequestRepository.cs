using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations
{
    public class SelfServiceRequestRepository : GenericRepository<TPLSelfServiceRequest>, ISelfServiceRequestRepository
    {
        public SelfServiceRequestRepository(HRSystemContext context) : base(context)
        {
        }
    }
}


