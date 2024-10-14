using Ecolume.Application.Services;
using Ecolume.Domain.Interfaces;
using Ecolume.Domain.Models;
using Moq;

namespace Ecolume.Tests.Services;

public class CategoryServiceTests
{
    private readonly ICategoryService _categoryService;
    private readonly Mock<IRepository<Category>> _mockRepository;

    public CategoryServiceTests()
    {
        _mockRepository = new Mock<IRepository<Category>>();
        _categoryService = new CategoryService(_mockRepository.Object);
    }

    #region CREATE

    [Fact]
    public async Task AddAsync_ShouldAddCategorySuccessfully()
    {
        var newCategory = new Category { Name = "New Category" };
        await _categoryService.AddAsync(newCategory);
        _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Category>()), Times.Once);
    }


    #endregion
    
    #region READ

    [Fact]
    public async Task GetAllAsync_ShouldReturnCategories()
    {
        var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Category 1" },
            new Category { Id = 2, Name = "Category 2" }
        };

        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categories);
        
        var result = await _categoryService.GetAllAsync();
        var enumerable = result.ToList();
        Assert.Equal(2, enumerable.Count());
        Assert.Equal("Category 1", enumerable.First().Name);
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnCategory_WhenCategoryExists()
    {
        var categoryId = 1;
        var mockCategory = new Category { Id = categoryId, Name = "Category 1" };
    
        _mockRepository.Setup(repo => repo.GetByIdAsync(categoryId))
            .ReturnsAsync(mockCategory);
        
        var result = await _categoryService.GetByIdAsync(categoryId);
        
        Assert.NotNull(result);
        Assert.Equal(categoryId, result.Id);
        Assert.Equal("Category 1", result.Name);
    }
    
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenCategoryDoesNotExist()
    {
        var categoryId = 1;
    
        _mockRepository.Setup(repo => repo.GetByIdAsync(categoryId))
            .ReturnsAsync((Category?)null);
        
        var result = await _categoryService.GetByIdAsync(categoryId);

        Assert.Null(result);
    }

    #endregion

    #region UPDATE
    
    [Fact]
    public async Task UpdateAsync_ShouldUpdateCategory_WhenCategoryExists()
    {
        var categoryId = 1;
        var existingCategory = new Category { Id = categoryId, Name = "Old Category" };
        var updatedCategory = new Category { Id = categoryId, Name = "Updated Category" };
    
        _mockRepository.Setup(repo => repo.GetByIdAsync(categoryId))
            .ReturnsAsync(existingCategory);

        var result = await _categoryService.UpdateAsync(categoryId, updatedCategory);
        Assert.NotNull(result);
        Assert.Equal("Updated Category", result.Name);
        _mockRepository.Verify(repo => repo.UpdateAsync(existingCategory), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNull_WhenCategoryDoesNotExist()
    {
        var categoryId = 1;
        var updatedCategory = new Category { Id = categoryId, Name = "Updated Category" };

        _mockRepository.Setup(repo => repo.GetByIdAsync(categoryId))
            .ReturnsAsync((Category?)null);
        
        var result = await _categoryService.UpdateAsync(categoryId, updatedCategory);
        
        Assert.Null(result);
    }
    
    #endregion

    #region DELETE

    [Fact]
    public async Task DeleteAsync_ShouldDeleteCategory_WhenCategoryExists()
    {
        var categoryId = 1;
        var existingCategory = new Category { Id = categoryId, Name = "Category to be deleted" };
    
        _mockRepository.Setup(repo => repo.GetByIdAsync(categoryId))
            .ReturnsAsync(existingCategory);
        
        var result = await _categoryService.DeleteAsync(categoryId);
        
        Assert.True(result);
        _mockRepository.Verify(repo => repo.DeleteAsync(existingCategory), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenCategoryDoesNotExist()
    {
        var categoryId = 1;

        _mockRepository.Setup(repo => repo.GetByIdAsync(categoryId))
            .ReturnsAsync((Category?)null);
        
        var result = await _categoryService.DeleteAsync(categoryId);
        Assert.False(result);
    }


    #endregion
}