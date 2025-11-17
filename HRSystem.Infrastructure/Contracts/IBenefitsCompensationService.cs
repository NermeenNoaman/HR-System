using HRSystem.BaseLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface IBenefitsCompensationService
    {
        Task<IEnumerable<BenefitsCompensationReadDto>> GetAllAsync();
        Task<BenefitsCompensationReadDto> GetByIdAsync(int id);
        Task<BenefitsCompensationReadDto> CreateAsync(BenefitsCompensationCreateDto dto);
        Task<bool> UpdateAsync(int id, BenefitsCompensationUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}


