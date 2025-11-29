using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations.Repositories
{
    public class BenefitsCompensationRepository : GenericRepository<TPLBenefitsCompensation>, IBenefitsCompensationRepository
    {
        public BenefitsCompensationRepository(HRSystemContext context) : base(context) { }
    }
}
