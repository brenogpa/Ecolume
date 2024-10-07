using Ecolume.Domain.Interfaces;
using Ecolume.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecolume.Controllers;

[ApiController]
[Route("category")]

public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
    {
        var categories = await _service.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _service.GetByIdAsync(id);
        return category == null ? NotFound() : Ok(category);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {
        await _service.AddAsync(category);
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, Category category)
    {
        if (id != category.Id)
            return BadRequest();
        await _service.UpdateAsync(id, category);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}