using DoctorAppointmentSystem.Application.DTOs;

namespace DoctorAppointmentSystem.Application.Interfaces
{
    public interface IDoctorService
    {
        Task CreateDoctorAsync(CreateDoctorDto dto);
        Task<List<SlotDto>> GetAvailableSlots(Guid doctorId, DateTime date);
    }
}
