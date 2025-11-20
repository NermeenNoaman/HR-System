using HRSystem.BaseLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface ISurveyResponseService
    {
        Task<IEnumerable<SurveyResponseReadDto>> GetAllAsync();
        Task<SurveyResponseReadDto> GetByIdAsync(int id);
        Task<SurveyResponseReadDto> CreateAsync(SurveyResponseCreateDto dto);
        Task<bool> UpdateAsync(int id, SurveyResponseUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}


