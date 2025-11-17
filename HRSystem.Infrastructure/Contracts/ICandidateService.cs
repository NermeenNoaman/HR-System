using HRSystem.BaseLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface ICandidateService
    {
        Task<IEnumerable<CandidateReadDto>> GetAllAsync();
        Task<CandidateReadDto> GetByIdAsync(int id);
        Task<CandidateReadDto> CreateAsync(CandidateCreateDto dto);
        Task<bool> UpdateAsync(int id, CandidateUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}


