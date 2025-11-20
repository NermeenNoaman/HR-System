using AutoMapper;
using HRSystem.BaseLibrary.DTOs;
using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Implementations.Services
{
    public class EvaluationCriteriaService : IEvaluationCriteriaService
    {
        private readonly IEvaluationCriteriaRepository _repository;
        private readonly IMapper _mapper;

        public EvaluationCriteriaService(IEvaluationCriteriaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EvaluationCriteriaReadDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<EvaluationCriteriaReadDto>>(entities);
        }

        public async Task<EvaluationCriteriaReadDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<EvaluationCriteriaReadDto>(entity);
        }

        public async Task<EvaluationCriteriaReadDto> CreateAsync(EvaluationCriteriaCreateDto dto)
        {
            var entity = _mapper.Map<TPLEvaluationCriterion>(dto);
            entity.CreatedDate = DateTime.Now;
            var created = await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<EvaluationCriteriaReadDto>(created);
        }

        public async Task<bool> UpdateAsync(int id, EvaluationCriteriaUpdateDto dto)
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

