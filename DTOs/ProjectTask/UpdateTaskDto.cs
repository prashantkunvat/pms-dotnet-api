using System.ComponentModel.DataAnnotations;
using PMS.Api.Enums;

namespace PMS.Api.DTOs.ProjectTask;

public class UpdateProjectTaskDto
{
    [Required]
    public int ProjectId {get; set;}

    [Required]
    [MaxLength(200)]
    public string Title {get; set;} = string.Empty;

    public string? Description {get; set;} = string.Empty;

    [Required]
    public Enums.TaskStatus Status {get; set;}

    [Required]
    public Enums.TaskPriority Priority {get; set;}

    public int SortOrder {get; set;}

    public DateTime? DueDate {get; set;}

}