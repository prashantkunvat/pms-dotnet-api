using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PMS.Api.DTOs.Project;
using PMS.Api.Interfaces;

namespace PMS.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _service;

    public ProjectsController(
        IProjectService service
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
        var projects = await _service.GetAllAsync(
            page,
            pageSize,
            search
        );

        return Ok(new
        {
            message = "Projects fetched successfully",
            data = projects
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        int id
    )
    {
        var project =
            await _service.GetByIdAsync(id);

        if (project is null)
        {
            return NotFound(new
            {
                message = "Project not found"
            });
        }

        return Ok(new
        {
            message = "Project fetched successfully",
            data = project
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProjectDto dto
    )
    {
        var project =
            await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = project.Id },
            new
            {
                message =
                    "Project created successfully",
                data = project
            }
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateProjectDto dto
    )
    {
        var updated =
            await _service.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound(new
            {
                message = "Project not found"
            });
        }

        return Ok(new
        {
            message =
                "Project updated successfully"
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
                message = "Project not found"
            });
        }

        return Ok(new
        {
            message =
                "Project deleted successfully"
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
                message = "Project not found"
            });
        }

        return Ok(new
        {
            message =
                "Project restored successfully"
        });
    }
}