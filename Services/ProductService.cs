using Microsoft.EntityFrameworkCore;
using PMS.Api.Data;
using PMS.Api.DTOs.Common;
using PMS.Api.DTOs.Product;
using PMS.Api.Interfaces;
using PMS.Api.Models;

public class ProductService : IProductService
{

    private readonly AppDbContext _contex;
    private readonly ICurrentUserService _currentUserService;


    public ProductService(
        AppDbContext context,
        ICurrentUserService currentUserService
        )
    {
        _contex = context;
        _currentUserService = currentUserService;
    }

    public async Task<PagedResultDto<ProductResponseDto>> GetAllAsync(
        int page,
        int pageSize,
        string? search
    )
    {
        IQueryable<Product> query = _contex.Products.Include(p => p.Category);


        if(!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    p.Name.ToLower().Contains(search.ToLower())
                );
            }

        var totalCount = await query.CountAsync();

        var items = await query 
            .OrderBy(p => p.SortOrder)
            .Skip((page - 1) * pageSize)    
            .Take(pageSize)
            .Select(p => new ProductResponseDto
            {

                Id = p.id,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                SKU = p.SKU,
                SortOrder = p.SortOrder,
                IsActive = p.IsActive,
                CreatedAt = p.CretedAt,
                UpdatedAt = p.UpdatedAt
            })
            .ToListAsync();


            return new PagedResultDto<ProductResponseDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
    }

    public async Task<ProductResponseDto?> GetByIdAsync(int id)
    {
        var product = await _contex.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.id == id);

        if(product == null)
            return null;

        return new ProductResponseDto
        {
            Id = product.id,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name,
            Description = product.Description,
            Price = product.Price,
            SKU = product.SKU,
            SortOrder = product.SortOrder,
            IsActive = product.IsActive,
            CreatedAt = product.CretedAt,
            UpdatedAt = product.UpdatedAt
        };
    }

    public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto)
    {
        var categoryExists = await _contex.ProductCategories
            .AnyAsync(c => c.Id == dto.CategoryId);

        if(!categoryExists)
            throw new Exception("Invalid category");

        var product = new Product
        {
            CategoryId =  dto.CategoryId,
            Name = dto.Name,
            Description = dto.Description,
            Price  = dto.Price,
            SKU = dto.SKU,
            SortOrder = dto.SortOrder,
            CreatedBy = _currentUserService.GetUserId()
        };

        _contex.Products.Add(product);

        await _contex.SaveChangesAsync();

        var category = await _contex.ProductCategories
            .FirstAsync(c => c.Id == dto.CategoryId);

        return new ProductResponseDto
        {
            Id = product.id,
            CategoryId = product.CategoryId,
            CategoryName = category.Name,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            SKU = product.SKU,
            SortOrder = product.SortOrder,
            IsActive = product.IsActive,
            CreatedAt = product.CretedAt,
            UpdatedAt = product.UpdatedAt
        };

    }

    public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
    {

        var categoryExists = await _contex.ProductCategories
            .AnyAsync(c => c.Id == dto.CategoryId);

        if(!categoryExists)
            throw new Exception("Invalid category");

        var product = await _contex.Products
            .FirstOrDefaultAsync(p => p.id == id);

        if(product == null)
            return false;

        product.CategoryId = dto.CategoryId;
        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.SKU = dto.SKU;
        product.SortOrder = dto.SortOrder;
        product.IsActive = dto.IsActive;
        product.UpdatedAt = DateTime.UtcNow;
        product.UpdatedBy = _currentUserService.GetUserId();

        await _contex.SaveChangesAsync();

        return true;

    }

    public async Task<bool> DeleteAsync (int id)
    {
        var product = await _contex.Products
            .FirstOrDefaultAsync(p => p.id == id);

        if(product is null)
            return false;

        product.IsDeleted = true;
        product.DeletedAt = DateTime.UtcNow;

        product.DeletedBy = _currentUserService.GetUserId();

        await _contex.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RestoreAsync(int id)
    {
        var product = await _contex.Products
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.id == id);

        if(product is null)
            return false;

        product.IsDeleted = false;
        product.DeletedAt = null;

        product.UpdatedAt =  DateTime.UtcNow;
        product.UpdatedBy = _currentUserService.GetUserId();

        await _contex.SaveChangesAsync();

        return true;
    }

}