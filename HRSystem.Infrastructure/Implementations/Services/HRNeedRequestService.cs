using AutoMapper;
using HRSystem.BaseLibrary.DTOs;
using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Implementations.Services
{
    public class HRNeedRequestService : IHRNeedRequestService
    {
        private readonly IHRNeedRequestRepository _repository;
        private readonly IMapper _mapper;

        public HRNeedRequestService(IHRNeedRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HRNeedRequestReadDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<HRNeedRequestReadDto>>(entities);
        }

        public async Task<HRNeedRequestReadDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<HRNeedRequestReadDto>(entity);
        }

        public async Task<HRNeedRequestReadDto> CreateAsync(HRNeedRequestCreateDto dto)
        {
            var entity = _mapper.Map<TPLHRNeedRequest>(dto);
            entity.CreatedDate = DateTime.Now;
            var created = await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<HRNeedRequestReadDto>(created);
        }

        public async Task<bool> UpdateAsync(int id, HRNeedRequestUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
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


