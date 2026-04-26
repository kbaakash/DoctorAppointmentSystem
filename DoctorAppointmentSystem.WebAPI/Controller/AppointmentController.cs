using DoctorAppointmentSystem.Application.DTOs;
using DoctorAppointmentSystem.Application.Interfaces;
using DoctorAppointmentSystem.Application.Services;
using DoctorAppointmentSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DoctorAppointmentSystem.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService AppointmentService;
        private readonly IPatientRepository PatientRepository;
        private readonly IDoctorService DoctorService;

        public AppointmentController(IAppointmentService appointmentService, IPatientRepository patientRepository,IDoctorService doctorService)
        {
            AppointmentService = appointmentService;
            PatientRepository = patientRepository;
            DoctorService = doctorService;
        }
        [Authorize(Roles = "Patient")]
        [HttpPost("book-appointment")]
        public async Task<IActionResult> BookAppointment(CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await AppointmentService.BookAppointment(userId, dto);
            return Ok("Appointment booked successfully");
        }
        [Authorize(Roles = "Patient")]
        [HttpGet("appointments/patient")]
        public async Task<IActionResult> GetMyAppointments()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var patient = await PatientRepository.GetByUserId(userId);

            var result = await AppointmentService.GetPatientAppointments(patient.Id);

            return Ok(result);
        }
        [Authorize(Roles = "Doctor")]
        [HttpGet("appointments/doctor")]
        public async Task<IActionResult> GetDoctorAppointments()
        {
            var doctorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await AppointmentService.GetDoctorAppointments(doctorId);

            return Ok(result);
        }
        [Authorize]
        [HttpGet("doctors/{doctorId}/available-slots")]
        public async Task<IActionResult> GetAvailableSlots(Guid doctorId, [FromQuery] DateTime date)
        {
            var slots = await DoctorService.GetAvailableSlots(doctorId, date);
            return Ok(slots);
        }
    }
}
