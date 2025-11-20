using HRSystem.BaseLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface IInterviewService
    {
        Task<IEnumerable<InterviewReadDto>> GetAllAsync();
        Task<InterviewReadDto> GetByIdAsync(int id);
        Task<InterviewReadDto> CreateAsync(InterviewCreateDto dto);
        Task<bool> UpdateAsync(int id, InterviewUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}


