using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskAPI.Models.Dtos;
using TaskAPI.Models.Model;
using TaskAPI.Services.Interfaces;

namespace TaskAPI.Services
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGeneratorService _tokenGeneratorService;

        public AuthService(UserManager<ApplicationUser> userManager, IJwtTokenGeneratorService tokenGeneratorService)
        {
            _userManager = userManager;
            _tokenGeneratorService = tokenGeneratorService;
        }

        public async Task<ReturnUserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.Email,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {

                var newUser = new ReturnUserDto()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,

                };



                return newUser;
            }

          
            return null;
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                
                return _tokenGeneratorService.GenerateToken(user);
            }

            
            return null;
        }
    }
}
