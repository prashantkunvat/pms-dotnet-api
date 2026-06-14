using Microsoft.AspNetCore.Mvc.RazorPages;
using PMS.Api.DTOs.Common;
using PMS.Api.DTOs.Product;

public interface IProductService
{
    Task<PagedResultDto<ProductResponseDto>> GetAllAsync(
        int page,
        int pageSize,
        string? search
    );

    Task<ProductResponseDto?> GetByIdAsync(int id);

    Task<ProductResponseDto> CreateAsync(CreateProductDto dto);

    Task<bool> UpdateAsync(int id, UpdateProductDto dto);
    Task<bool> DeleteAsync(int id);

    Task<bool> RestoreAsync(int id);

}