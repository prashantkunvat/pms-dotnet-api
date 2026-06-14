using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PMS.Api.DTOs.Product;
using PMS.Api.Interfaces;

namespace PMS.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(
        IProductService service
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
        var products = await _service.GetAllAsync(
            page,
            pageSize,
            search
        );

        return Ok(new
        {
            message = "Products fetched successfully",
            data = products
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        int id
    )
    {
        var product =
            await _service.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound(new
            {
                message = "Product not found"
            });
        }

        return Ok(new
        {
            message = "Product fetched successfully",
            data = product
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductDto dto
    )
    {
        var product =
            await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id },
            new
            {
                message =
                    "Product created successfully",
                data = product
            }
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateProductDto dto
    )
    {
        var updated =
            await _service.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound(new
            {
                message = "Product not found"
            });
        }

        return Ok(new
        {
            message =
                "Product updated successfully"
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id
    )
    {
        var deleted =
            await _service.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Product not found"
            });
        }

        return Ok(new
        {
            message =
                "Product deleted successfully"
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
                message = "Product not found"
            });
        }

        return Ok(new
        {
            message =
                "Product restored successfully"
        });
    }
}