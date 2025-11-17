using AutoMapper;
using HRSystem.BaseLibrary.DTOs;
using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Implementations.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _repository;
        private readonly IMapper _mapper;

        public SurveyService(ISurveyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SurveyReadDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SurveyReadDto>>(entities);
        }

        public async Task<SurveyReadDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<SurveyReadDto>(entity);
        }

        public async Task<SurveyReadDto> CreateAsync(SurveyCreateDto dto)
        {
            var entity = _mapper.Map<TPLSurvey>(dto);
            // CreatedDate is already mapped from DTO (DateOnly), but we also set UpdatedDate
            entity.UpdatedDate = null; // New entity, no update yet
            var created = await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<SurveyReadDto>(created);
        }

        public async Task<bool> UpdateAsync(int id, SurveyUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            entity.UpdatedDate = DateTime.Now;
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            await _repository.DeleteAsync(entity);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}

