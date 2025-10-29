using HRSystem.BaseLibrary.DTOs;
using HRSystem.BaseLibrary.Models;
using HRSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace HRSystem_Wizer_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HRSystemContext _hRSystemContext;

        public AuthController(HRSystemContext hRSystemContext)
        {
            _hRSystemContext = hRSystemContext;

        }


        [HttpPost("Register")]
        public ActionResult<TPLUser> Register(UserRegisterDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (_hRSystemContext.TPLUsers.Any(user => user.Username == request.Username))
                return BadRequest("Username already exists.");
            if (_hRSystemContext.TPLEmployees.FirstOrDefault(
                user => user.EmployeeID == request.EmployeeId) == null)
            {
                return BadRequest("Employee not found. Provide a valid EmployeeId.");
            }


            TPLUser user = new TPLUser
            {
                Username = request.Username,
                EmployeeID = request.EmployeeId,
                Role = "Employee"

            };


            user.PasswordHash = new PasswordHasher<TPLUser>()
                .HashPassword(user, request.Password);

            _hRSystemContext.TPLUsers.Add(user);
            _hRSystemContext.SaveChanges();

            return Ok(new
            {
                username = user.Username,
                role = user.Role,
                message = "User registered successfully"
            });
        }
        [HttpPost("login")]
        public ActionResult<String> Login(UserLoginDto request)
        {
            TPLUser user = _hRSystemContext.TPLUsers.FirstOrDefault(user => user.Username == request.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            var result = new PasswordHasher<TPLUser>()
            .VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (result == PasswordVerificationResult.Failed)
                return BadRequest("Password incorrect");

            string token = "Here you will create JWT token";

            return Ok(new { Token = token });


        }


    }
}
