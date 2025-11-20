using HRSystem.BaseLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface IDocumentManagementService
    {
        Task<IEnumerable<DocumentManagementReadDto>> GetAllAsync();
        Task<DocumentManagementReadDto> GetByIdAsync(int id);
        Task<DocumentManagementReadDto> CreateAsync(DocumentManagementCreateDto dto);
        Task<bool> UpdateAsync(int id, DocumentManagementUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}


