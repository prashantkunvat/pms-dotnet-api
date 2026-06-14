using PMS.Api.DTOs.Common;
using PMS.Api.DTOs.ProjectTask;

namespace PMS.Api.Interfaces;

public interface IProjectTaskService
{
    Task<PagedResultDto<ProjectTaskResponseDto>> GetAllAsync(
        int page,
        int pagesize,
        string? search
    );

    Task<ProjectTaskResponseDto?> GetByIdAsync(int id);

    Task<ProjectTaskResponseDto> CreateAsync(CreateProjectTaskDto dto);

    Task<bool> UpdateAsync(int id, UpdateProjectTaskDto dto);

    Task<bool> DeleteAsync(int id);

    Task<bool> RestoreAsync(int id);
}