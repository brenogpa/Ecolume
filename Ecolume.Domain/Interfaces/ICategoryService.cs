using Ecolume.Domain.Models;

namespace Ecolume.Domain.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task AddAsync(Category category);
    Task<Category?> UpdateAsync(int id, Category updatedCategory);
    Task<bool> DeleteAsync(int id);
}