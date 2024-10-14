using Ecolume.Application.Services;
using Ecolume.Domain.Interfaces;
using Ecolume.Domain.Models;
using Moq;

namespace Ecolume.Tests.Services;

public class ProductServiceTests
{
    private readonly IProductService _productService;
    private readonly Mock<IRepository<Product>> _mockRepository;

    public ProductServiceTests()
    {
        _mockRepository = new Mock<IRepository<Product>>();
        _productService = new ProductService(_mockRepository.Object);
    }

    #region CREATE

    [Fact]
    public async Task AddAsync_ShouldAddProductSuccessfully()
    {
        var newProduct = new Product { Name = "New Product", Price = 100 };
        await _productService.AddAsync(newProduct);
        _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
    }


    #endregion
    
    #region READ

    [Fact]
    public async Task GetAllAsync_ShouldReturnProducts()
    {
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1" },
            new Product { Id = 2, Name = "Product 2" }
        };

        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
        
        var result = await _productService.GetAllAsync();
        var enumerable = result.ToList();
        Assert.Equal(2, enumerable.Count());
        Assert.Equal("Product 1", enumerable.First().Name);
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
    {
        var productId = 1;
        var mockProduct = new Product { Id = productId, Name = "Product 1" };
    
        _mockRepository.Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync(mockProduct);
        
        var result = await _productService.GetByIdAsync(productId);
        
        Assert.NotNull(result);
        Assert.Equal(productId, result.Id);
        Assert.Equal("Product 1", result.Name);
    }
    
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
    {
        var productId = 1;
    
        _mockRepository.Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync((Product?)null);
        
        var result = await _productService.GetByIdAsync(productId);

        Assert.Null(result);
    }

    #endregion

    #region UPDATE
    
    [Fact]
    public async Task UpdateAsync_ShouldUpdateProduct_WhenProductExists()
    {
        var productId = 1;
        var existingProduct = new Product { Id = productId, Name = "Old Product" };
        var updatedProduct = new Product { Id = productId, Name = "Updated Product" };
    
        _mockRepository.Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync(existingProduct);

        var result = await _productService.UpdateAsync(productId, updatedProduct);
        Assert.NotNull(result);
        Assert.Equal("Updated Product", result.Name);
        _mockRepository.Verify(repo => repo.UpdateAsync(existingProduct), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNull_WhenProductDoesNotExist()
    {
        var productId = 1;
        var updatedProduct = new Product { Id = productId, Name = "Updated Product" };

        _mockRepository.Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync((Product?)null);
        
        var result = await _productService.UpdateAsync(productId, updatedProduct);
        
        Assert.Null(result);
    }
    
    #endregion

    #region DELETE

    [Fact]
    public async Task DeleteAsync_ShouldDeleteProduct_WhenProductExists()
    {
        var productId = 1;
        var existingProduct = new Product { Id = productId, Name = "Product to be deleted" };
    
        _mockRepository.Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync(existingProduct);
        
        var result = await _productService.DeleteAsync(productId);
        
        Assert.True(result);
        _mockRepository.Verify(repo => repo.DeleteAsync(existingProduct), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenProductDoesNotExist()
    {
        var productId = 1;

        _mockRepository.Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync((Product?)null);
        
        var result = await _productService.DeleteAsync(productId);
        Assert.False(result);
    }


    #endregion
}