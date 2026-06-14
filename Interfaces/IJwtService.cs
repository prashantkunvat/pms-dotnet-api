using PMS.Api.Models;

namespace PMS.Api.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}