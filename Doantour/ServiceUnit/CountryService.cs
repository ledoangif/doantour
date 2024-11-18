using Doantour.DTO;
using Doantour.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Doantour.ServiceUnit
{
    public class CountryService
    {
        private readonly Hachutravelcontext _context;

        public CountryService(Hachutravelcontext hachutravelcontext)
        {
            _context = hachutravelcontext;
        }

        public async Task<ResponseModel> InsertAsync(Country obj)
        {
            var existingCountryName = await _context.Country.AnyAsync(r => r.CountryName == obj.CountryName && r.IsDeleted == false);
            if (existingCountryName)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Quốc gia đã tồn tại!"
                };
            }
            obj.CreateDate = DateTime.Now;
            obj.UpdateDate = DateTime.Now;
            await _context.Country.AddAsync(obj);
            await _context.SaveChangesAsync();
            return new ResponseModel
            {
                Status = true,
                Message = "Thêm quốc gia thành công!"
            };
        }

        public async Task<ResponseModel> UpdateAsync(int id, Country obj)
        {
            var existingEntity = await _context.Country.FindAsync(id);
            if (existingEntity == null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Thông tin không tồn tại!"
                };
            }

            obj.UpdateDate = DateTime.Now;
            _context.Country.Update(existingEntity);
            await _context.SaveChangesAsync();
            return new ResponseModel
            {
                Status = true,
                Message = "Cập nhật thành công!"
            };
        }


        public async Task<ResponseModel> DeleteAsync(int id)
        {
            var existingEntity = await _context.Country.FindAsync(id);
            if (existingEntity == null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Thông tin không tồn tại!"
                };
            }   
            
            if (existingEntity.IsDeleted == true)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Thông tin đã bị xoá!"
                };
            }

            existingEntity.IsDeleted = true;
            existingEntity.UpdateDate = DateTime.Now;
            var item = _context.Country.Update(existingEntity);
            await _context.SaveChangesAsync();
            return new ResponseModel
            {
                Status = true,
                Message = "Xoá thành công!"
            };
        }

        public async Task<ResponseModel> SearchAsync(string name)
        {
            if (name == null || name.Length < 1)
            {
                return new ResponseModel
                {
                    Status = true,
                    Message = "Thông tin này không được để trống!"
                };
            }
            Expression<Func<Country, bool>> predicate = a => a.CountryName == name;
            var searchResult = await _context.Country.Where(predicate).ToListAsync();

            if (!searchResult.Any())
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Không tìm thấy kết quả nào!"
                };
            }

            return new ResponseModel
            {
                Status = true,
                Message = "Tìm kiếm thành công"
            };
        }

    }
}
