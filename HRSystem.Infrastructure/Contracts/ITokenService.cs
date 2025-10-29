using HRSystem.BaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HRSystem.Infrastructure.Contracts { 
    internal interface ITokenService { 
        Task<string> GenerateJwtTokenAsync(TPLUser user);
        Task<string> GenerateRefreshTokenAsync();
    } 
}