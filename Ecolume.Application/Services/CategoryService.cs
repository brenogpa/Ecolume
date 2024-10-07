using Ecolume.Domain.Interfaces;
using Ecolume.Domain.Models;

namespace Ecolume.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _repository;

    public CategoryService(IRepository<Category> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Category category)
    {
        await _repository.AddAsync(category);
    }

    public async Task<Category?> UpdateAsync(int id, Category updatedCategory)
    {
        var existingCategory = await GetByIdAsync(id);
        if (existingCategory == null)
            return null;
        
        if (!string.IsNullOrWhiteSpace(updatedCategory.Name))
            existingCategory.Name = updatedCategory.Name;
        
        await _repository.UpdateAsync(existingCategory);
        return existingCategory;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await GetByIdAsync(id);
        if (category == null)
            return false;
        await _repository.DeleteAsync(category);
        return true;
    }
}