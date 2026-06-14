using System.ComponentModel.DataAnnotations;

namespace PMS.Api.DTOs.ProductCategory;

public class UpdateProductCategoryDto
{
    [Required]
    public string Name {get; set;} = string.Empty;

    [StringLength(500)]
    public string? Description {get; set;}

    [Range(0, 1000)]
    public int SortOrder {get; set;}

    public bool IsActive {get; set;}
}