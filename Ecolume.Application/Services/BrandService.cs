using Ecolume.Domain.Interfaces;
using Ecolume.Domain.Models;

namespace Ecolume.Application.Services;

public class BrandService : IBrandService
{
    private readonly IRepository<Brand> _repository;

    public BrandService(IRepository<Brand> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Brand>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Brand?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Brand brand)
    {
        await _repository.AddAsync(brand);
    }

    public async Task<Brand?> UpdateAsync(int id, Brand updatedBrand)
    {
        var existingBrand = await GetByIdAsync(id);
        if (existingBrand == null)
            return null;
        
        if (!string.IsNullOrWhiteSpace(updatedBrand.Name))
            existingBrand.Name = updatedBrand.Name;
        
        await _repository.UpdateAsync(existingBrand);
        return existingBrand;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var brand = await GetByIdAsync(id);
        if (brand == null)
            return false;
        await _repository.DeleteAsync(brand);
        return true;
    }
}