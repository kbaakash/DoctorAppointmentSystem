using DoctorAppointmentSystem.Application.DTOs;
using DoctorAppointmentSystem.Application.Interfaces;
using DoctorAppointmentSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.WebAPI.Controller
{
    
    [Route("api/[controller]")]
    [ApiController]
   
    public class AdminController : ControllerBase
    {
        private readonly IDoctorService DoctorService;

        public AdminController(IDoctorService doctorService)
        {
            DoctorService = doctorService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("create-doctor")]
        public async Task<IActionResult> CreateDoctor(CreateDoctorDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await DoctorService.CreateDoctorAsync(dto);
            return Ok("Doctor Created Successfully");
        }
    }

}
    