using HRSystem.BaseLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface ISurveyService
    {
        Task<IEnumerable<SurveyReadDto>> GetAllAsync();
        Task<SurveyReadDto> GetByIdAsync(int id);
        Task<SurveyReadDto> CreateAsync(SurveyCreateDto dto);
        Task<bool> UpdateAsync(int id, SurveyUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}


