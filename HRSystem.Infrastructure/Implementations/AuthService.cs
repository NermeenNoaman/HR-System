using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Contracts;
using HRSystem.Infrastructure.Contracts.Requests;
using HRSystem.Infrastructure.Contracts.Responses;
using HRSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Implementations
{
    internal class AuthService: IAuthService
    {
        
            private readonly HRSystemContext _context;
            private readonly ITokenService _tokenService;

            public AuthService(HRSystemContext context, ITokenService tokenService)
            {
                _context = context;
                _tokenService = tokenService;
            }

            public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
            {
                var existingUser = await _context.TPLUsers
                    .FirstOrDefaultAsync(u => u.Username == request.Username);

                if (existingUser != null)
                    throw new Exception("Username already exists");

                var hashedPassword = HashPassword(request.Password);

                var newUser = new TPLUser
                {
                    EmployeeID = request.EmployeeID,
                    Username = request.Username,
                    PasswordHash = hashedPassword,
                    Role = request.Role
                };

                _context.TPLUsers.Add(newUser);
                await _context.SaveChangesAsync();

                var token = await _tokenService.GenerateJwtTokenAsync(newUser);
                var refreshToken = await _tokenService.GenerateRefreshTokenAsync();

                return new AuthResponse
                {
                    Username = newUser.Username,
                    Role = newUser.Role,
                    Token = token,
                    RefreshToken = refreshToken,
                    TokenExpires = DateTime.UtcNow.AddMinutes(30)
                };
            }

            public async Task<AuthResponse> LoginAsync(LoginRequest request)
            {
                var user = await _context.TPLUsers
                    .FirstOrDefaultAsync(u => u.Username == request.Username);

                if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
                    throw new Exception("Invalid username or password");

                var token = await _tokenService.GenerateJwtTokenAsync(user);
                var refreshToken = await _tokenService.GenerateRefreshTokenAsync();

                return new AuthResponse
                {
                    Username = user.Username,
                    Role = user.Role,
                    Token = token,
                    RefreshToken = refreshToken,
                    TokenExpires = DateTime.UtcNow.AddMinutes(30)
                };
            }

            public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
            {
                // هنا ممكن تضيفي لوجيك التحقق من الريفريش توكن
                throw new NotImplementedException();
            }

            public async Task<bool> LogoutAsync(string username)
            {
                // هنا ممكن تمسحي أو تبطلي التوكن
                return await Task.FromResult(true);
            }

            private string HashPassword(string password)
            {
                using (var sha = SHA256.Create())
                {
                    var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(bytes);
                }
            }

            private bool VerifyPassword(string password, string storedHash)
            {
                var hashOfInput = HashPassword(password);
                return hashOfInput == storedHash;
            }
        }
}
