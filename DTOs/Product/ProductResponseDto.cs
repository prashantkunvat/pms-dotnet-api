
namespace PMS.Api.DTOs.Product;

public class ProductResponseDto
{
    public int Id {get; set;}

    public int CategoryId {get; set;}

    public string CategoryName {get; set;} = string.Empty;

    public string Name {get; set;} = string.Empty;

    public string? Description {get; set;}

    public decimal Price {get; set;}

    public string SKU {get; set;} = string.Empty;

    public int SortOrder {get; set;}

    public bool IsActive {get; set;}

    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}

}