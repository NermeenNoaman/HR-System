using HRSystem.BaseLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface IJobApplicationService
    {
        Task<IEnumerable<JobApplicationReadDto>> GetAllAsync();
        Task<JobApplicationReadDto> GetByIdAsync(int id);
        Task<JobApplicationReadDto> CreateAsync(JobApplicationCreateDto dto);
        Task<bool> UpdateAsync(int id, JobApplicationUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}


