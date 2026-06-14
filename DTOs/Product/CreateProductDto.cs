
using System.ComponentModel.DataAnnotations;

namespace PMS.Api.DTOs.Product;

public class CreateProductDto
{   

    [Required]
    public int CategoryId {get; set;}

    [Required]
    [MaxLength(200)]
    public string Name {get; set;} = string.Empty;

    public string? Description {get; set;}

    [Range(0.01, 999999)]
    public decimal Price {get; set;}

    [Required]
    public string SKU {get; set;} = string.Empty;

    public int SortOrder {get; set;}

}