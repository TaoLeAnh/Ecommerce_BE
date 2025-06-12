using EcommerceBackend.Models;
using Microsoft.EntityFrameworkCore;
using EcommerceBackend.Data;

namespace EcommerceBackend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _unitOfWork.Categories.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _unitOfWork.Categories.GetByIdAsync(id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            category.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.CompleteAsync();
            return category;
        }

        public async Task<Category> UpdateAsync(int id, Category category)
        {
            var existing = await _unitOfWork.Categories.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = category.Name;
            existing.Description = category.Description;

            _unitOfWork.Categories.Update(existing);
            await _unitOfWork.CompleteAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null) return false;

            _unitOfWork.Categories.Remove(category);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}