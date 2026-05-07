using DoctorAppointmentSystem.Application.DTOs;
namespace DoctorAppointmentSystem.Application.Interfaces
{
    public interface IAuthService
    {
         public Task<string> LoginAsync(LoginDto dto);
        public Task RegisterAsync(RegisterPatientDto dto);
    }
}
