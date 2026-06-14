using Microsoft.EntityFrameworkCore;
using PMS.Api.Data;
using PMS.Api.DTOs.Common;
using PMS.Api.DTOs.ProjectTask;
using PMS.Api.Interfaces;
using PMS.Api.Models;

namespace PMS.Api.Services;

public class ProjectTaskService : IProjectTaskService
{
    private readonly AppDbContext _context;

    private readonly ICurrentUserService _currentUserService;

    public ProjectTaskService(
        AppDbContext context,
        ICurrentUserService currentUserService
        
        )
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<PagedResultDto<ProjectTaskResponseDto>> GetAllAsync(
        int page,
        int pagesize,
        string? search
    )
    {
        IQueryable<ProjectTask> query = _context.ProjectTasks.Include(t => t.Project);

        if(!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(t =>
        
                t.Title.ToLower().Contains(search.ToLower())
            
            );
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(t => t.SortOrder)
            .Skip((page -1 ) * pagesize)
            .Take(pagesize)
            .Select(t => new ProjectTaskResponseDto
            {
                Id = t.Id,
                ProjectId = t.ProjectId,
                ProjectName = t.Project.Name,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                SortOrder = t.SortOrder,
                DueDate = t.DueDate,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }
            
            ).ToListAsync();

        
        return new PagedResultDto<ProjectTaskResponseDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pagesize
        };
    }

    public async Task<ProjectTaskResponseDto> GetByIdAsync(int id)
    {
        var task = await _context.ProjectTasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id);

        if(task == null)
            return null;

        return new ProjectTaskResponseDto
        {
                Id = task.Id,
                ProjectId = task.ProjectId,
                ProjectName = task.Project.Name,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                SortOrder = task.SortOrder,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
        };
    }

    public async Task<ProjectTaskResponseDto> CreateAsync(CreateProjectTaskDto dto)
    {
        var projectExists = await _context.Projects.AnyAsync(p => p.Id == dto.ProjectId);

        if (!projectExists)
        {
            throw new Exception("Project doesnt exists.");
        }

        var task = new ProjectTask
        {
            ProjectId = dto.ProjectId,
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            Priority = dto.Priority,
            SortOrder = dto.SortOrder,
            DueDate = dto.DueDate,
            CreatedBy = _currentUserService.GetUserId()
        };


        _context.ProjectTasks.Add(task);

        await _context.SaveChangesAsync();

        var project = await _context.Projects.FirstAsync(p => p.Id == dto.ProjectId);

        return new ProjectTaskResponseDto
        {
                Id = task.Id,
                ProjectId = task.ProjectId,
                ProjectName = project.Name,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                SortOrder = task.SortOrder,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateProjectTaskDto dto)
    {

        var projectExists = await _context.Projects.AnyAsync(p => p.Id == dto.ProjectId);

        if (!projectExists)
        {
            throw new Exception("Project doesnt exists.");
        }

        var task = await _context.ProjectTasks.FirstOrDefaultAsync(t => t.Id == id);

        if(task is null)   
            return false;

        task.ProjectId = dto.ProjectId;
        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Status = dto.Status;
        task.Priority = dto.Priority;
        task.SortOrder = dto.SortOrder;
        task.DueDate = dto.DueDate;
        task.UpdatedAt = DateTime.UtcNow;
        task.UpdatedBy = _currentUserService.GetUserId();

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.ProjectTasks.FirstOrDefaultAsync(t => t.Id == id);

        if(task is null)
            return false;

        task.IsDeleted = true;
        task.DeletedAt = DateTime.UtcNow;
        task.DeletedBy = _currentUserService.GetUserId();

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RestoreAsync(int id)
    {
        var task = await _context.ProjectTasks.IgnoreQueryFilters().FirstOrDefaultAsync(t => t.Id == id);

        if(task is null)
            return false;

        task.IsDeleted = false;
        task.DeletedAt = null;

        task.UpdatedAt = DateTime.UtcNow;
        task.UpdatedBy = _currentUserService.GetUserId();

        await _context.SaveChangesAsync();

        return true;
    }
    
}