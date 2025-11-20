using HRSystem.BaseLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface ISelfServiceRequestService
    {
        Task<IEnumerable<SelfServiceRequestReadDto>> GetAllAsync();
        Task<SelfServiceRequestReadDto> GetByIdAsync(int id);
        Task<SelfServiceRequestReadDto> CreateAsync(SelfServiceRequestCreateDto dto);
        Task<bool> UpdateAsync(int id, SelfServiceRequestUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}


