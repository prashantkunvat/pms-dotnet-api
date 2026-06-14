namespace PMS.Api.Models;

public class Product
{
    public int id {get; set;}

    public int CategoryId {get; set;}

    public string Name {get; set;} = string.Empty;

    public string? Description {get; set;}

    public decimal Price {get; set;}

    public string SKU {get; set;} = string.Empty;

    public int SortOrder {get; set;}

    public bool IsActive {get; set;}

    public bool IsDeleted {get; set;}

    public DateTime CretedAt {get; set;} = DateTime.UtcNow;

    public int? CreatedBy {get; set;}

    public DateTime UpdatedAt {get; set;}

    public int? UpdatedBy {get; set;}

    public DateTime? DeletedAt {get; set;}

    public int? DeletedBy {get; set;}

    // Naviagation Property
    public ProductCategory Category {get; set;} = null!;

}