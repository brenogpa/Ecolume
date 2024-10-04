using Ecolume.Domain.Interfaces;
using Ecolume.Domain.Models;

namespace Ecolume.Application.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _repository;

    public ProductService(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Product> AddAsync(Product product)
    {
        await _repository.AddAsync(product);
        return product;
    }

    public async Task<Product?> UpdateAsync(int id, Product updatedProduct)
    {
        var existingProduct = await _repository.GetByIdAsync(id);

        if (existingProduct == null) 
            return null;
        
        if (!string.IsNullOrWhiteSpace(updatedProduct.Name))
            existingProduct.Name = updatedProduct.Name;
        
        if (!string.IsNullOrWhiteSpace(updatedProduct.Description))
            existingProduct.Description = updatedProduct.Description;

        if (updatedProduct.Price != default)
            existingProduct.Price = updatedProduct.Price;

        if (updatedProduct.BrandId != default)
            existingProduct.BrandId = updatedProduct.BrandId;

        if (updatedProduct.CategoryId != default)
            existingProduct.CategoryId = updatedProduct.CategoryId;

        await _repository.UpdateAsync(existingProduct);

        return existingProduct;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null)
            return false;

        await _repository.DeleteAsync(product);
        return true;
    }
}