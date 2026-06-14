using System.ComponentModel.DataAnnotations;
using PMS.Api.Enums;

namespace PMS.Api.DTOs.Project;

public class CreateProjectDto
{
    [Required]
    [MaxLength(200)]
    public string Name {get; set;} = string.Empty;
    
    public string? Description {get; set;}

    public ProjectStatus Status {get; set;} = ProjectStatus.Pending;

    public DateTime StartDate {get; set;}

    public DateTime? EndDate {get; set;}

    

}