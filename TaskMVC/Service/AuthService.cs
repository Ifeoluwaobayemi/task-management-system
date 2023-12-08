using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TaskMVC.Service
{
    using global::TaskMVC.Models;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
 

    namespace TaskMVC.Service
    {
        public class AuthService : BaseService
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            public AuthService(HttpClient client, IHttpContextAccessor httpContextAccessor, IConfiguration config)
                : base(client, httpContextAccessor, config)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ReturnUserDto> Register(RegisterDto registerDto)
            {
                const string apiUrl = "api/auth/register";
                return await MakeRequest<ReturnUserDto, RegisterDto>(apiUrl, HttpMethod.Post.Method, registerDto);
            }

            public async Task<string> Login(LoginDto loginDto)
            {
                const string apiUrl = "api/auth/login";
            var result   =    await MakeRequest<string, LoginDto>(apiUrl, HttpMethod.Post.Method, loginDto);
                var token = result;
                var claims = new List<Claim>
            {
               
            new("JwtToken", token)
          
             };

                var identity = new ClaimsIdentity(claims, "cookie");
                await _httpContextAccessor?.HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity))!;

                return result;
            }
        }
    }

}
