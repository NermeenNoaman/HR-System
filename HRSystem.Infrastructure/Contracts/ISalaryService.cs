using HRSystem.BaseLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface ISalaryService
    {
        Task<IEnumerable<SalaryReadDto>> GetAllAsync();
        Task<SalaryReadDto> GetByIdAsync(int id);
        Task<SalaryReadDto> CreateAsync(SalaryCreateDto dto);
        Task<bool> UpdateAsync(int id, SalaryUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}


