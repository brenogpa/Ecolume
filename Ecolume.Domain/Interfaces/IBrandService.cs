using Ecolume.Domain.Models;

namespace Ecolume.Domain.Interfaces;

public interface IBrandService
{
    Task<IEnumerable<Brand>> GetAllAsync();
    Task<Brand?> GetByIdAsync(int id);
    Task AddAsync(Brand brand);
    Task<Brand?> UpdateAsync(int id, Brand updatedBrand);
    Task<bool> DeleteAsync(int id);
}