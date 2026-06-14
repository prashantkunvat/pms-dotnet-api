namespace PMS.Api.Models;

public class ProductCategory
{
    public int Id {get; set;}

    public string Name  {get; set;} = string.Empty;

    public string? Description {get; set;}

    public int SortOrder {get; set;}

    public bool IsActive {get; set;} = true;

    public bool IsDeleted {get; set;} = false;

    public DateTime? DeletedAt {get; set;}

    public int? DeletedBy {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public int? CreatedBy {get; set;}
    public DateTime? UpdatedAt {get; set;}
    
    public int? UpdatedBy {get; set;}

    // Navigation Collection

    public List<Product> Products {get; set;} = new();
}