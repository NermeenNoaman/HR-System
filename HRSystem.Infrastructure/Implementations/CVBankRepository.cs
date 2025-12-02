using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations.Repositories
{
    public class CVBankRepository : GenericRepository<TPLCVBank>, ICVBankRepository
    {
        public CVBankRepository(HRSystemContext context) : base(context) { }
    }
}
