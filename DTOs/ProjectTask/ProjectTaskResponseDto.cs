using PMS.Api.Enums;

namespace PMS.Api.DTOs.ProjectTask;

public class ProjectTaskResponseDto
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public string ProjectName { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }
 
    public Enums.TaskStatus Status { get; set; }

    public TaskPriority Priority { get; set; }

    public int SortOrder { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}