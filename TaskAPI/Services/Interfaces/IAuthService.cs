
using TaskAPI.Models.Dtos;

namespace TaskAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ReturnUserDto> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
    }
}