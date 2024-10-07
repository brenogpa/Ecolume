using Ecolume.Domain.Interfaces;
using Ecolume.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecolume.Controllers;

[ApiController]
[Route("brand")]

public class BrandController : ControllerBase
{
    private readonly IBrandService _service;

    public BrandController(IBrandService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
    {
        var brands = await _service.GetAllAsync();
        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Brand>> GetBrand(int id)
    {
        var brand = await _service.GetByIdAsync(id);
        return brand == null ? NotFound() : Ok(brand);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Brand>> PostBrand(Brand brand)
    {
        await _service.AddAsync(brand);
        return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBrand(int id, Brand brand)
    {
        if (id != brand.Id)
            return BadRequest();
        await _service.UpdateAsync(id, brand);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}