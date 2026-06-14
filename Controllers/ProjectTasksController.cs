using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PMS.Api.DTOs.ProjectTask;
using PMS.Api.Interfaces;

namespace PMS.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProjectTasksController : ControllerBase
{
    private readonly IProjectTaskService _service;

    public ProjectTasksController(
        IProjectTaskService service
    )
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] string? search = null
    )
    {
        var tasks = await _service.GetAllAsync(
            page,
            pageSize,
            search
        );

        return Ok(new
        {
            message = "Tasks fetched successfully",
            data = tasks
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        int id
    )
    {
        var task =
            await _service.GetByIdAsync(id);

        if (task is null)
        {
            return NotFound(new
            {
                message = "Task not found"
            });
        }

        return Ok(new
        {
            message = "Task fetched successfully",
            data = task
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProjectTaskDto dto
    )
    {
        var task =
            await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = task.Id },
            new
            {
                message =
                    "Task created successfully",
                data = task
            }
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateProjectTaskDto dto
    )
    {
        var updated =
            await _service.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound(new
            {
                message = "Task not found"
            });
        }

        return Ok(new
        {
            message =
                "Task updated successfully"
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id
    )
    {
        var deleted =
            await _service.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Task not found"
            });
        }

        return Ok(new
        {
            message =
                "Task deleted successfully"
        });
    }

    [HttpPut("{id}/restore")]
    public async Task<IActionResult> Restore(
        int id
    )
    {
        var restored =
            await _service.RestoreAsync(id);

        if (!restored)
        {
            return NotFound(new
            {
                message = "Task not found"
            });
        }

        return Ok(new
        {
            message =
                "Task restored successfully"
        });
    }
}