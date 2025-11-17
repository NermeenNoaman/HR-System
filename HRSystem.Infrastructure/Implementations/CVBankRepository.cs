using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations
{
    public class CVBankRepository : GenericRepository<TPLCVBank>, ICVBankRepository
    {
        public CVBankRepository(HRSystemContext context) : base(context)
        {
        }
    }
}


