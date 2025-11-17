using AutoMapper;
using HRSystem.BaseLibrary.DTOs;
using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Implementations.Services
{
    public class RecruitmentPortalService : IRecruitmentPortalService
    {
        private readonly IRecruitmentPortalRepository _repository;
        private readonly IMapper _mapper;

        public RecruitmentPortalService(IRecruitmentPortalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecruitmentPortalReadDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<RecruitmentPortalReadDto>>(entities);
        }

        public async Task<RecruitmentPortalReadDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<RecruitmentPortalReadDto>(entity);
        }

        public async Task<RecruitmentPortalReadDto> CreateAsync(RecruitmentPortalCreateDto dto)
        {
            var entity = _mapper.Map<TPLRecruitmentPortal>(dto);
            entity.CreatedDate = DateTime.Now;
            var created = await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<RecruitmentPortalReadDto>(created);
        }

        public async Task<bool> UpdateAsync(int id, RecruitmentPortalUpdateDto dto)
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

