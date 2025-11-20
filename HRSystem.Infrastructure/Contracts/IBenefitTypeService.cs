using HRSystem.BaseLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface IBenefitTypeService
    {
        Task<IEnumerable<BenefitTypeReadDto>> GetAllAsync();
        Task<BenefitTypeReadDto> GetByIdAsync(int id);
        Task<BenefitTypeReadDto> CreateAsync(BenefitTypeCreateDto dto);
        Task<bool> UpdateAsync(int id, BenefitTypeUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}


