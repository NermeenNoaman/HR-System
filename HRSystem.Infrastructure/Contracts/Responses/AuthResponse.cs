using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts.Responses
{
    internal class AuthResponse
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
