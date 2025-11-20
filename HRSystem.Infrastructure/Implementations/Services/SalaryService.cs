using AutoMapper;
using HRSystem.BaseLibrary.DTOs;
using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Implementations.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryRepository _repository;
        private readonly IMapper _mapper;

        public SalaryService(ISalaryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SalaryReadDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SalaryReadDto>>(entities);
        }

        public async Task<SalaryReadDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<SalaryReadDto>(entity);
        }

        public async Task<SalaryReadDto> CreateAsync(SalaryCreateDto dto)
        {
            var entity = _mapper.Map<LKPSalary>(dto);
            // Calculate NetSalary
            entity.NetSalary = entity.BaseSalary + entity.Bonus - entity.Deductions;
            entity.CreatedDate = DateTime.Now;
            var created = await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<SalaryReadDto>(created);
        }

        public async Task<bool> UpdateAsync(int id, SalaryUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            // Recalculate NetSalary
            entity.NetSalary = entity.BaseSalary + entity.Bonus - entity.Deductions;
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

