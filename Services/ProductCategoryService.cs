using Microsoft.EntityFrameworkCore;
using PMS.Api.Data;
using PMS.Api.DTOs.Common;
using PMS.Api.DTOs.ProductCategory;
using PMS.Api.Interfaces;
using PMS.Api.Models;
namespace PMS.Api.Services;

public class ProductCategoryService : IProductCategoryService
{

    private readonly AppDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public ProductCategoryService(
        AppDbContext context,
        ICurrentUserService currentUserService
    )
    {
        _context = context;
        _currentUserService = currentUserService;
    }


    // Get All the Product Categories
    public async Task<PagedResultDto <ProductCategoryResponseDto>> GetAllAsync(
        int page,
        int pageSize,
        string? search
    )
    {

        IQueryable<ProductCategory>query = _context.ProductCategories;

        if(!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c => 
            c.Name.ToLower().Contains(search.ToLower()));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(c => c.SortOrder)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new ProductCategoryResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                SortOrder = c.SortOrder,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync();


            return new PagedResultDto<ProductCategoryResponseDto> 
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };

    }

    // Get the single product category by id
    public async Task<ProductCategoryResponseDto> GetByIdAsync(int id)
    {
        var cateogry = await _context.ProductCategories.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

        if(cateogry == null)
            return null;

        return new ProductCategoryResponseDto
        {
            Id = cateogry.Id,
            Name = cateogry.Name,
            Description = cateogry.Description,
            SortOrder = cateogry.SortOrder,
            IsActive = cateogry.IsActive,
            CreatedAt = cateogry.CreatedAt
        };
    }

    // Create Product Category

    public async Task<ProductCategoryResponseDto> CreateAsync(CreateProductCategoryDto dto)
    {
        var category = new ProductCategory
        {
            Name = dto.Name,
            Description = dto.Description,
            SortOrder = dto.SortOrder,
            CreatedBy = _currentUserService.GetUserId()
        };

        _context.ProductCategories.Add(category);

        await _context.SaveChangesAsync();

        return new ProductCategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            SortOrder = category.SortOrder,
            IsActive = category.IsActive,
            CreatedAt = category.CreatedAt
        };
    }

    // Update Product Category 

    public async Task<bool> UpdateAsync(int id, UpdateProductCategoryDto dto)
    {
        var category = await _context.ProductCategories.FirstOrDefaultAsync(c => c.Id == id );

        if(category == null)
            return false;

        category.Name = dto.Name;
        category.Description = dto.Description;
        category.SortOrder = dto.SortOrder;
        category.IsActive = dto.IsActive;
        category.UpdatedBy = _currentUserService.GetUserId();
        category.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    // Delete Product Category

    public async Task<bool> DeleteAsync (int id)
    {
        var category = await _context.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);

        if(category == null)
            return false;

        category.IsDeleted = true;
        category.DeletedAt = DateTime.UtcNow;
        category.DeletedBy = _currentUserService.GetUserId();

        await _context.SaveChangesAsync();

        return true;
    }

    // Restore Category

    public async Task<bool> RestoreAsync (int id)
    {
        var category = await _context.ProductCategories
        .IgnoreQueryFilters()
        .FirstOrDefaultAsync(c => c.Id == id);


        if(category == null)
            return false;

        category.IsDeleted = false;
        category.DeletedAt = null;

        category.UpdatedAt = DateTime.UtcNow;
        category.UpdatedBy = _currentUserService.GetUserId();

        await _context.SaveChangesAsync();

        return true;
    }
    
}
