using AutoMapper;
using HRSystem.BaseLibrary.DTOs;
using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Implementations.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _repository;
        private readonly IMapper _mapper;

        public JobApplicationService(IJobApplicationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JobApplicationReadDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<JobApplicationReadDto>>(entities);
        }

        public async Task<JobApplicationReadDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<JobApplicationReadDto>(entity);
        }

        public async Task<JobApplicationReadDto> CreateAsync(JobApplicationCreateDto dto)
        {
            var entity = _mapper.Map<LkpJobApplication>(dto);
            entity.ApplyDate = DateTime.Now;
            var created = await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<JobApplicationReadDto>(created);
        }

        public async Task<bool> UpdateAsync(int id, JobApplicationUpdateDto dto)
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

