using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations
{
    public class RecruitmentPortalRepository : GenericRepository<TPLRecruitmentPortal>, IRecruitmentPortalRepository
    {
        public RecruitmentPortalRepository(HRSystemContext context) : base(context)
        {
        }
    }
}


