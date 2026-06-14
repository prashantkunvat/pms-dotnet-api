using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PMS.Api.DTOs.ProductCategory;
using PMS.Api.Interfaces;

namespace PMS.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductCategoriesController : ControllerBase
{
    private readonly IProductCategoryService _service;

    public ProductCategoriesController(
        IProductCategoryService service
    )
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] string? search = null
    )
    {
        var categories = await _service.GetAllAsync(
            page,
            pageSize,
            search
        );

        return Ok(new
        {
            message = "Categories fetched successfully",
            data = categories
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _service.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound(new
            {
                message = "Category not found"
            });
        }

        return Ok(new
        {
            message = "Category fetched successfully",
            data = category
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductCategoryDto dto
    )
    {
        var category = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = category.Id },
            new
            {
                message =
                    "Category created successfully",
                data = category
            }
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateProductCategoryDto dto
    )
    {
        var updated = await _service.UpdateAsync(
            id,
            dto
        );

        if (!updated)
        {
            return NotFound(new
            {
                message = "Category not found"
            });
        }

        return Ok(new
        {
            message =
                "Category updated successfully"
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id
    )
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Category not found"
            });
        }

        return Ok(new
        {
            message =
                "Category deleted successfully"
        });
    }

    [HttpPut("{id}/restore")]
    public async Task<IActionResult> Restore(
        int id
    )
    {
        var restored =
            await _service.RestoreAsync(id);

        if (!restored)
        {
            return NotFound(new
            {
                message = "Category not found"
            });
        }

        return Ok(new
        {
            message =
                "Category restored successfully"
        });
    }
}