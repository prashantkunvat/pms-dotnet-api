using Microsoft.EntityFrameworkCore;
using PMS.Api.Data;
using PMS.Api.DTOs.Common;
using PMS.Api.DTOs.Project;
using PMS.Api.Interfaces;
using PMS.Api.Models;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public ProjectService(
        AppDbContext context,
        ICurrentUserService currentUserService
        
        )
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<PagedResultDto<ProjectResponseDto>> GetAllAsync(

        int page,
        int pageSize,
        string? search
    )
    {
        IQueryable<Project> query = _context.Projects;

        if(!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => 
            
                p.Name.ToLower().Contains(search.ToLower()

            ));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select( p => new ProjectResponseDto

            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Status = p.Status,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt

            }
            )
            .ToListAsync();

        return new PagedResultDto<ProjectResponseDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }   


    public async Task<ProjectResponseDto> GetByIdAsync(int id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

        if(project is null)
            return null;

        return new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt
        };
    }    

    public async Task<ProjectResponseDto> CreateAsync(CreateProjectDto dto)
    {
        var project = new Project
        {
            Name = dto.Name,
            Description = dto.Description,
            Status = dto.Status,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            CreatedBy = _currentUserService.GetUserId()
        };

        _context.Projects.Add(project);

        await _context.SaveChangesAsync();

        return new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateProjectDto dto)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

        if(project is null)
            return false;

        project.Name = dto.Name;
        project.Description = dto.Description;
        project.Status = dto.Status;
        project.StartDate = dto.StartDate;
        project.EndDate = dto.EndDate;
        project.UpdatedAt = DateTime.UtcNow;
        project.UpdatedBy = _currentUserService.GetUserId();

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

        if(project is null)
            return false;

        project.IsDeleted = true;
        project.DeletedAt = DateTime.UtcNow;
        project.DeletedBy = _currentUserService.GetUserId();

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RestoreAsync(int id)
    {
        var project = await _context.Projects.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == id);

        if(project is null)
            return false;

        project.IsDeleted = false;
        project.DeletedAt = null;

        project.UpdatedAt = DateTime.UtcNow;
        project.UpdatedBy = _currentUserService.GetUserId();

        await _context.SaveChangesAsync();

        return true;
    }
}