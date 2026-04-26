using DoctorAppointmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Application.Interfaces
{
    public interface IPatientRepository
    {
       Task AddAsync(Patient patient);
        Task<Patient> GetByUserId(Guid userId);
    }
}
