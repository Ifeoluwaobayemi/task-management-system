
using TaskAPI.Models.Model;

namespace TaskAPI.Services.Interfaces
{
    public interface IJwtTokenGeneratorService
    {

        string GenerateToken(ApplicationUser user);
    }
}