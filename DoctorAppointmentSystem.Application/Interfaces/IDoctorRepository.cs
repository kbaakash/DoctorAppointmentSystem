using DoctorAppointmentSystem.Domain.Entities;
namespace DoctorAppointmentSystem.Application.Interfaces
{
    public interface IDoctorRepository
    {
        Task AddAsync(Doctor doctor);
        Task<Doctor> GetByIdAsync(Guid id);
        Task<List<Doctor>> GetAllAsync();
        
    }
}
