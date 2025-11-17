using HRSystem.BaseLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface ICVBankService
    {
        Task<IEnumerable<CVBankReadDto>> GetAllAsync();
        Task<CVBankReadDto> GetByIdAsync(int id);
        Task<CVBankReadDto> CreateAsync(CVBankCreateDto dto);
        Task<bool> UpdateAsync(int id, CVBankUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}


