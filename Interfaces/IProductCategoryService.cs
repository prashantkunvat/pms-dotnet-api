using PMS.Api.DTOs.ProductCategory;
using PMS.Api.DTOs.Common;
using PMS.Api.Services;

namespace PMS.Api.Interfaces;
public interface IProductCategoryService
{
    Task<PagedResultDto<ProductCategoryResponseDto>> GetAllAsync(
        int page,
        int pageSize,
        string? search
    );

    Task<ProductCategoryResponseDto?> GetByIdAsync(int id);

    Task<ProductCategoryResponseDto> CreateAsync(CreateProductCategoryDto dto);

    Task<bool> UpdateAsync(int id, UpdateProductCategoryDto dto);

    Task<bool> DeleteAsync(int id);

    Task<bool> RestoreAsync(int id);
}