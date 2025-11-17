using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations
{
    public class HRNeedRequestRepository : GenericRepository<TPLHRNeedRequest>, IHRNeedRequestRepository
    {
        public HRNeedRequestRepository(HRSystemContext context) : base(context)
        {
        }
    }
}


