using DoctorAppointmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Application.Interfaces
{
    public interface IDoctorRepository
    {
        Task AddAsync(Doctor doctor);
        Task<Doctor> GetByIdAsync(Guid id);
        Task<List<Doctor>> GetAllAsync();
        
    }
}
