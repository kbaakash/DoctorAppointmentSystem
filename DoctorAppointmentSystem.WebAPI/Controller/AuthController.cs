using DoctorAppointmentSystem.Application.DTOs;
using DoctorAppointmentSystem.Application.Interfaces;
using DoctorAppointmentSystem.Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService AuthService;
        public AuthController(IAuthService authService)
        {
            AuthService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
           var token =await AuthService.LoginAsync(dto);
            return Ok(token);
        }
         
        
        [HttpPost("register-patient")]
        public async Task<IActionResult> Register(RegisterPatientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await AuthService.RegisterAsync(dto);
            return Ok("Patient registered successfully");
        }
    }
}
