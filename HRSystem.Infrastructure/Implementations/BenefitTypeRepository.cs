using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations
{
    public class BenefitTypeRepository : GenericRepository<LkpBenefitType>, IBenefitTypeRepository
    {
        public BenefitTypeRepository(HRSystemContext context) : base(context)
        {
        }
    }
}


