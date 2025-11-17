using HRSystem.BaseLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface IJobService
    {
        Task<IEnumerable<JobReadDto>> GetAllAsync();
        Task<JobReadDto> GetByIdAsync(int id);
        Task<JobReadDto> CreateAsync(JobCreateDto dto);
        Task<bool> UpdateAsync(int id, JobUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}


