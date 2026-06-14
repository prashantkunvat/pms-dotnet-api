using PMS.Api.Enums;

namespace PMS.Api.Models;

public class Project
{
    
    public int Id {get; set;}

    public string Name {get; set;} = string.Empty;

    public string? Description {get; set;}

    public ProjectStatus Status {get; set;}

    public DateTime StartDate {get; set;}

    public DateTime? EndDate {get; set;}

    public bool IsDeleted {get; set;} = false;

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public int? CreatedBy {get; set;}

    public DateTime? UpdatedAt {get; set;}

    public int? UpdatedBy {get; set;}

    public DateTime? DeletedAt {get; set;}

    public int? DeletedBy {get; set;}

    public List<ProjectTask> ProjectTask {get; set;} = new();
}