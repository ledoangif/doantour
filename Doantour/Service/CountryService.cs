using AutoMapper;
using Doantour.DTO;
using Doantour.Helpers;
using Doantour.Helpers.Page;
using Doantour.Models;
using Doantour.Repository;
using Doantour.Respository;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Doantour.Service
{
    public class CountryService : IBaseService<Country, CountryDTO>
    {
        private readonly CountryRepository _CountryRepository;
        private readonly TourRepository _TourRepository;

        private readonly IMapper _mapper;

        public CountryService(IMapper mapper, IServiceProvider serviceProvider)
        {
            var _repositoryFactory = serviceProvider.GetService<RepositoryFactory>();
            _CountryRepository = _repositoryFactory.CountryRepository;
            _TourRepository = _repositoryFactory.TourRepository;
            _mapper = mapper;
        }

        public async Task<List<CountryDTO>> ToListAsync()
        {
            var entities = await _CountryRepository.ToListAsync();

            var CountryListDTO = _mapper.Map<List<CountryDTO>>(entities).ToList();

            return CountryListDTO;
        }
        public async Task<List<CountryDTO>> Top4Country()
        {
            var entities = await _CountryRepository.ToListAsync();
            var CountryListDTO = _mapper.Map<List<CountryDTO>>(entities).Take(4).ToList();
            return CountryListDTO;
        }
        public async Task<List<CountryDTO>> Next6Countries()
        {
            var entities = await _CountryRepository.ToListAsync();

            var CountryListDTO = _mapper.Map<List<CountryDTO>>(entities).Skip(4).Take(6).ToList();

            return CountryListDTO;
        }


        public async Task<List<CountryDTO>> SearchAsync(Expression<Func<Country, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new BadHttpRequestException("Predicate is null!");
            }

            var searchResult = await _CountryRepository.SearchAsync(predicate);
            var resultDTO = _mapper.Map<List<CountryDTO>>(searchResult);
            return resultDTO;
        }
        public async Task<PageResult<Country>> GetAllPagination(Pagination? pagination)
        {
            var entities = await _CountryRepository.ToListAsync();
            var result = PageResult<Country>.ToPageResult(pagination, entities);
            pagination.TotalCount = entities.Count();
            return new PageResult<Country>(pagination, result);
        }
        public async Task<CountryDTO> FindAsync(int id)
        {
            var entity = await _CountryRepository.FindAsync(id);
            if (entity == null)
            {
                return new CountryDTO();
            }

            var CountryDto = _mapper.Map<CountryDTO>(entity);

            if (CountryDto == null)
            {
                return new CountryDTO();
            }

            return CountryDto;
        }

        public async Task<CountryDTO> InsertAsync(CountryDTO obj)
        {
            var existingCountryName = await _CountryRepository.SelectAsync(r => r.CountryName == obj.CountryName);
            if (existingCountryName)
            {
                return null;
            }
            obj.CreateDate = DateTime.Now;
            obj.UpdateDate = DateTime.Now;
            var objMap = _mapper.Map<Country>(obj);
            await _CountryRepository.InsertAsync(objMap);
            return obj;
        }

        public async Task<CountryDTO> UpdateAsync(int id, CountryDTO obj)
        {
            var existingEntity = await _CountryRepository.FindAsync(id);
            if (existingEntity == null)
            {
                throw new BadHttpRequestException("Entity not found.");
            }

            if (id != obj.Id)
            {
                throw new BadHttpRequestException("Mismatched Id.");
            }

            obj.UpdateDate = DateTime.Now;
            _mapper.Map(obj, existingEntity);
            await _CountryRepository.UpdateAsync(existingEntity);
            return obj;
        }


        public async Task<CountryDTO> DeleteAsync(int id)
        {
            var existingEntity = await _CountryRepository.FindAsync(id);
            if (existingEntity == null)
            {
                throw new BadHttpRequestException("Entity not found.");
            }

            if (existingEntity == null)
            {
                return null;
            }

            existingEntity.IsDeleted = true;
            existingEntity.UpdateDate = DateTime.Now;
            var item = await _CountryRepository.UpdateAsync(existingEntity);
            return _mapper.Map<CountryDTO>(item);
        }
        public async Task<string> UploadFile([FromForm] IFormFile ImageFile)
        {
            string url = await Upload.Upfile(ImageFile);
            return url;
        }

    }
}
