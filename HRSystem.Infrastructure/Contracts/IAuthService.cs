using HRSystem.Infrastructure.Contracts.Requests;
using HRSystem.Infrastructure.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    internal interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RefreshTokenAsync(string refreshToken);
        Task<bool> LogoutAsync(string username);
    }
}
