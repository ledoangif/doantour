﻿using AutoMapper;
using Doantour.DTO;
using Doantour.Helpers.Page;
using Doantour.Models;
using Doantour.Repository;
using Doantour.Respository;
using System.Linq.Expressions;

namespace Doantour.Service
{
    public class HotelService : IBaseService<Hotel, HotelDTO>
    {
        private readonly HotelRepository _HotelRepository;
        private readonly IMapper _mapper;

        public HotelService(IMapper mapper, IServiceProvider serviceProvider)
        {
            var _repositoryFactory = serviceProvider.GetService<RepositoryFactory>();
            _HotelRepository = _repositoryFactory.HotelRepository;
            _mapper = mapper;
        }

        public async Task<List<HotelDTO>> ToListAsync()
        {
            var entities = await _HotelRepository.ToListAsync();

            var HotelListDTO = _mapper.Map<List<HotelDTO>>(entities).ToList();

            return HotelListDTO;
        }

        public async Task<List<HotelDTO>> SearchAsync(Expression<Func<Hotel, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new BadHttpRequestException("Predicate is null!");
            }

            var searchResult = await _HotelRepository.SearchAsync(predicate);
            var resultDTO = _mapper.Map<List<HotelDTO>>(searchResult);
            return resultDTO;
        }

        public async Task<HotelDTO> FindAsync(int id)
        {
            var entity = await _HotelRepository.FindAsync(id);
            if (entity == null)
            {
                return new HotelDTO();
            }

            var HotelDto = _mapper.Map<HotelDTO>(entity);

            if (HotelDto == null)
            {
                return new HotelDTO();
            }

            return HotelDto;
        }

        public async Task<HotelDTO> InsertAsync(HotelDTO obj)
        {
            var existingHotelName = await _HotelRepository.SelectAsync(r => r.HotelName == obj.HotelName);
            if (existingHotelName)
            {
                return null;
            }
            obj.CreateDate = DateTime.Now;
            obj.UpdateDate = DateTime.Now;
            var objMap = _mapper.Map<Hotel>(obj);
            await _HotelRepository.InsertAsync(objMap);
            return obj;
        }

        public async Task<HotelDTO> UpdateAsync(int id, HotelDTO obj)
        {
            var existingEntity = await _HotelRepository.FindAsync(id);
            if (existingEntity == null)
            {
                throw new BadHttpRequestException("Entity not found.");
            }

            if (id != obj.Id)
            {
                throw new BadHttpRequestException("Mismatched Id.");
            }
            obj.UpdateDate = DateTime.Now;
            obj.CreateDate = existingEntity.CreateDate;
            _mapper.Map(obj, existingEntity);
            await _HotelRepository.UpdateAsync(existingEntity);
            return obj;
        }

        public async Task<HotelDTO> DeleteAsync(int id)
        {
            var existingEntity = await _HotelRepository.FindAsync(id);
            if (existingEntity == null)
            {
                throw new BadHttpRequestException("Entity not found.");
            }
            existingEntity.IsDeleted = true;
            existingEntity.UpdateDate = DateTime.Now;
            var item = await _HotelRepository.UpdateAsync(existingEntity);
            return _mapper.Map<HotelDTO>(item);
        }

        public async Task<PageResult<Hotel>> GetAllPagination(Pagination? pagination)
        {
            var entities = await _HotelRepository.ToListAsync();
            var result = PageResult<Hotel>.ToPageResult(pagination, entities);
            pagination.TotalCount = entities.Count();
            return new PageResult<Hotel>(pagination, result);
        }
    }
}
