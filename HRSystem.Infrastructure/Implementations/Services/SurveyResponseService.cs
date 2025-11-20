using AutoMapper;
using HRSystem.BaseLibrary.DTOs;
using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Implementations.Services
{
    public class SurveyResponseService : ISurveyResponseService
    {
        private readonly ISurveyResponseRepository _repository;
        private readonly IMapper _mapper;

        public SurveyResponseService(ISurveyResponseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SurveyResponseReadDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SurveyResponseReadDto>>(entities);
        }

        public async Task<SurveyResponseReadDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<SurveyResponseReadDto>(entity);
        }

        public async Task<SurveyResponseReadDto> CreateAsync(SurveyResponseCreateDto dto)
        {
            var entity = _mapper.Map<TPLSurvey_Response>(dto);
            entity.CreatedDate = DateTime.Now;
            var created = await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<SurveyResponseReadDto>(created);
        }

        public async Task<bool> UpdateAsync(int id, SurveyResponseUpdateDto dto)
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

