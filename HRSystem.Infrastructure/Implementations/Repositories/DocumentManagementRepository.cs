using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Implementations.Repositories
{
    public class DocumentManagementRepository : GenericRepository<TPLDocumentManagement>, IDocumentManagementRepository
    {
        public DocumentManagementRepository(HRSystemContext context) : base(context) { }
    }
}
