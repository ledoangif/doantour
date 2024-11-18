using Doantour.DTO;
using Doantour.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Doantour.ServiceUnit
{
    public class TourService
    {
        private readonly Hachutravelcontext _context;

        public TourService(Hachutravelcontext hachutravelcontext)
        {
            _context = hachutravelcontext;
        }

        public async Task<ResponseModel> InsertAsync(Tour obj)
        {
            var existingTourName = await _context.Tour.AnyAsync(r => r.NameTour == obj.NameTour && r.IsDeleted == false);
            if (existingTourName)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Thêm thất bại!"
                };
            }
            obj.CreateDate = DateTime.Now;
            obj.UpdateDate = DateTime.Now;
            await _context.Tour.AddAsync(obj);
            await _context.SaveChangesAsync();
            return new ResponseModel
            {
                Status = false,
                Message = "Thêm thành công!"
            };
        }

        public async Task<ResponseModel> UpdateAsync(int id, Tour obj)
        {
            var existingEntity = await _context.Tour.FindAsync(id);
            if (existingEntity == null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Thông tin không tồn tại!"
                };
            }

            obj.UpdateDate = DateTime.Now;
            _context.Tour.Update(existingEntity);
            await _context.SaveChangesAsync();
            return new ResponseModel
            {
                Status = true,
                Message = "Cập nhật thành công!"
            };
        }


        public async Task<ResponseModel> DeleteAsync(int id)
        {
            var existingEntity = await _context.Tour.FindAsync(id);
            if (existingEntity == null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Thông tin không tồn tại!"
                };
            }

            existingEntity.IsDeleted = true;
            existingEntity.UpdateDate = DateTime.Now;
            var item = _context.Tour.Update(existingEntity);
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
            Expression<Func<Tour, bool>> predicate = a => a.TourName == name;
            var searchResult = await _context.Tour.Where(predicate).ToListAsync();

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