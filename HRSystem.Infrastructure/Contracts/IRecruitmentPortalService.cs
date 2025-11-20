using HRSystem.BaseLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface IRecruitmentPortalService
    {
        Task<IEnumerable<RecruitmentPortalReadDto>> GetAllAsync();
        Task<RecruitmentPortalReadDto> GetByIdAsync(int id);
        Task<RecruitmentPortalReadDto> CreateAsync(RecruitmentPortalCreateDto dto);
        Task<bool> UpdateAsync(int id, RecruitmentPortalUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}


