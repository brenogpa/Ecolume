using Ecolume.Application.Services;
using Ecolume.Domain.Interfaces;
using Ecolume.Domain.Models;
using Moq;

namespace Ecolume.Tests.Services;

public class BrandServiceTests
{
        private readonly IBrandService _brandService;
    private readonly Mock<IRepository<Brand>> _mockRepository;

    public BrandServiceTests()
    {
        _mockRepository = new Mock<IRepository<Brand>>();
        _brandService = new BrandService(_mockRepository.Object);
    }

    #region CREATE

    [Fact]
    public async Task AddAsync_ShouldAddBrandSuccessfully()
    {
        var newBrand = new Brand { Name = "New Brand" };
        await _brandService.AddAsync(newBrand);
        _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Brand>()), Times.Once);
    }


    #endregion
    
    #region READ

    [Fact]
    public async Task GetAllAsync_ShouldReturnBrand()
    {
        var brands = new List<Brand>
        {
            new Brand { Id = 1, Name = "Brand 1" },
            new Brand { Id = 2, Name = "Brand 2" }
        };

        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(brands);
        
        var result = await _brandService.GetAllAsync();
        var enumerable = result.ToList();
        Assert.Equal(2, enumerable.Count());
        Assert.Equal("Brand 1", enumerable.First().Name);
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnBrand_WhenBrandExists()
    {
        var brandId = 1;
        var mockBrand = new Brand { Id = brandId, Name = "Brand 1" };
    
        _mockRepository.Setup(repo => repo.GetByIdAsync(brandId))
            .ReturnsAsync(mockBrand);
        
        var result = await _brandService.GetByIdAsync(brandId);
        
        Assert.NotNull(result);
        Assert.Equal(brandId, result.Id);
        Assert.Equal("Brand 1", result.Name);
    }
    
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenBrandDoesNotExist()
    {
        var brandId = 1;
    
        _mockRepository.Setup(repo => repo.GetByIdAsync(brandId))
            .ReturnsAsync((Brand?)null);
        
        var result = await _brandService.GetByIdAsync(brandId);

        Assert.Null(result);
    }

    #endregion

    #region UPDATE
    
    [Fact]
    public async Task UpdateAsync_ShouldUpdateBrand_WhenBrandExists()
    {
        var brandId = 1;
        var existingBrand = new Brand { Id = brandId, Name = "Old Brand" };
        var updatedBrand = new Brand { Id = brandId, Name = "Updated Brand" };
    
        _mockRepository.Setup(repo => repo.GetByIdAsync(brandId))
            .ReturnsAsync(existingBrand);

        var result = await _brandService.UpdateAsync(brandId, updatedBrand);
        Assert.NotNull(result);
        Assert.Equal("Updated Brand", result.Name);
        _mockRepository.Verify(repo => repo.UpdateAsync(existingBrand), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNull_WhenBrandDoesNotExist()
    {
        var brandId = 1;
        var updatedBrand = new Brand { Id = brandId, Name = "Updated Brand" };

        _mockRepository.Setup(repo => repo.GetByIdAsync(brandId))
            .ReturnsAsync((Brand?)null);
        
        var result = await _brandService.UpdateAsync(brandId, updatedBrand);
        
        Assert.Null(result);
    }
    
    #endregion

    #region DELETE

    [Fact]
    public async Task DeleteAsync_ShouldDeleteBrand_WhenBrandExists()
    {
        var brandId = 1;
        var existingBrand = new Brand { Id = brandId, Name = "Brand to be deleted" };
    
        _mockRepository.Setup(repo => repo.GetByIdAsync(brandId))
            .ReturnsAsync(existingBrand);
        
        var result = await _brandService.DeleteAsync(brandId);
        
        Assert.True(result);
        _mockRepository.Verify(repo => repo.DeleteAsync(existingBrand), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenBrandDoesNotExist()
    {
        var brandId = 1;

        _mockRepository.Setup(repo => repo.GetByIdAsync(brandId))
            .ReturnsAsync((Brand?)null);
        
        var result = await _brandService.DeleteAsync(brandId);
        Assert.False(result);
    }


    #endregion
}