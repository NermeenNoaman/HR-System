using HRSystem.BaseLibrary.Models;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface ISelfServiceRequestRepository : IGenericRepository<TPLSelfServiceRequest>
    {
        // Custom queries for SelfServiceRequests (if any)
    }
}
