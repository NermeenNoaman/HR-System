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
    public class SelfServiceRequestService : ISelfServiceRequestService
    {
        private readonly ISelfServiceRequestRepository _repository;
        private readonly IMapper _mapper;

        public SelfServiceRequestService(ISelfServiceRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SelfServiceRequestReadDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SelfServiceRequestReadDto>>(entities);
        }

        public async Task<SelfServiceRequestReadDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<SelfServiceRequestReadDto>(entity);
        }

        public async Task<SelfServiceRequestReadDto> CreateAsync(SelfServiceRequestCreateDto dto)
        {
            var entity = _mapper.Map<TPLSelfServiceRequest>(dto);
            entity.CreatedDate = DateTime.Now;
            var created = await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<SelfServiceRequestReadDto>(created);
        }

        public async Task<bool> UpdateAsync(int id, SelfServiceRequestUpdateDto dto)
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

