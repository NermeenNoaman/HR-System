using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations.Repositories
{
    public class CandidateRepository : GenericRepository<TPLCandidate>, ICandidateRepository
    {
        public CandidateRepository(HRSystemContext context) : base(context) { }
    }
}
