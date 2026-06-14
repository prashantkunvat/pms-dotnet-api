using PMS.Api.DTOs.Common;
using PMS.Api.DTOs.Project;

public interface IProjectService
{
    Task<PagedResultDto<ProjectResponseDto>> GetAllAsync(
        int page,
        int pageSize,
        string? search
    );

    Task<ProjectResponseDto?> GetByIdAsync (int id);

    Task<ProjectResponseDto> CreateAsync(CreateProjectDto dto);

    Task<bool> UpdateAsync(int id, UpdateProjectDto dto);

    Task<bool> DeleteAsync(int id);

    Task<bool> RestoreAsync(int id);
}