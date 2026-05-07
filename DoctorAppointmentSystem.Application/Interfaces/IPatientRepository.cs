using DoctorAppointmentSystem.Domain.Entities;

namespace DoctorAppointmentSystem.Application.Interfaces
{
    public interface IPatientRepository
    {
       Task AddAsync(Patient patient);
        Task<Patient> GetByUserId(Guid userId);
    }
}
