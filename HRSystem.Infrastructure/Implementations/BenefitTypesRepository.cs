using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations.Repositories
{
    public class BenefitTypesRepository : GenericRepository<LkpBenefitType>, IBenefitTypesRepository
    {
        public BenefitTypesRepository(HRSystemContext context) : base(context) { }
    }
}
