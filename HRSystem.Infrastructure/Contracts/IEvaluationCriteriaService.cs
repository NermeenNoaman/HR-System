using HRSystem.BaseLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface IEvaluationCriteriaService
    {
        Task<IEnumerable<EvaluationCriteriaReadDto>> GetAllAsync();
        Task<EvaluationCriteriaReadDto> GetByIdAsync(int id);
        Task<EvaluationCriteriaReadDto> CreateAsync(EvaluationCriteriaCreateDto dto);
        Task<bool> UpdateAsync(int id, EvaluationCriteriaUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}


