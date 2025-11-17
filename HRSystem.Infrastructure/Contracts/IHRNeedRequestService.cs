using HRSystem.BaseLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface IHRNeedRequestService
    {
        Task<IEnumerable<HRNeedRequestReadDto>> GetAllAsync();
        Task<HRNeedRequestReadDto> GetByIdAsync(int id);
        Task<HRNeedRequestReadDto> CreateAsync(HRNeedRequestCreateDto dto);
        Task<bool> UpdateAsync(int id, HRNeedRequestUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}


