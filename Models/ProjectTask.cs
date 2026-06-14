using PMS.Api.Enums;

namespace PMS.Api.Models;

public class ProjectTask
{
    public int Id {get; set;}

    public int ProjectId {get; set;}

    public string Title {get; set;} = string.Empty;

    public string? Description {get;set;}

    public Enums.TaskStatus Status {get; set;} = Enums.TaskStatus.Pending;

    public TaskPriority Priority {get; set;} = Enums.TaskPriority.Low;

    public int SortOrder {get; set;}

    public bool IsDeleted {get; set;} = false;

    public DateTime? DueDate {get; set;}

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public int? CreatedBy {get; set;}

    public DateTime? UpdatedAt {get; set;}

    public int? UpdatedBy {get; set;}

    public DateTime? DeletedAt {get; set;}

    public int? DeletedBy {get; set;}

    // Navigation Property
    public Project Project {get; set;} = null!;

}