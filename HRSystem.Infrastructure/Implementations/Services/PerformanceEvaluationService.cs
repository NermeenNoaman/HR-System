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
    public class PerformanceEvaluationService : IPerformanceEvaluationService
    {
        private readonly IPerformanceEvaluationRepository _repository;
        private readonly IMapper _mapper;

        public PerformanceEvaluationService(IPerformanceEvaluationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PerformanceEvaluationReadDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PerformanceEvaluationReadDto>>(entities);
        }

        public async Task<PerformanceEvaluationReadDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<PerformanceEvaluationReadDto>(entity);
        }

        public async Task<PerformanceEvaluationReadDto> CreateAsync(PerformanceEvaluationCreateDto dto)
        {
            var entity = _mapper.Map<TPLPerformanceEvaluation>(dto);
            entity.CreatedDate = DateTime.Now;
            var created = await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<PerformanceEvaluationReadDto>(created);
        }

        public async Task<bool> UpdateAsync(int id, PerformanceEvaluationUpdateDto dto)
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

