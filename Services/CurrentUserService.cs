using System.Security.Claims;
using PMS.Api.Interfaces;

namespace PMS.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService (IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetUserId()
    {
        var userId =  _httpContextAccessor
            .HttpContext?
            .User
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        return int.Parse(userId!);
    }
}