using HRSystem.BaseLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface IPerformanceEvaluationService
    {
        Task<IEnumerable<PerformanceEvaluationReadDto>> GetAllAsync();
        Task<PerformanceEvaluationReadDto> GetByIdAsync(int id);
        Task<PerformanceEvaluationReadDto> CreateAsync(PerformanceEvaluationCreateDto dto);
        Task<bool> UpdateAsync(int id, PerformanceEvaluationUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}


