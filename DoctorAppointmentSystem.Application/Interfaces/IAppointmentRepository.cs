using DoctorAppointmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Application.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<int>> GetBookedSlotIds(Guid doctorId, DateTime date);
        Task<bool> IsSlotBooked(Guid doctorId, DateTime date, int slotId);
        Task AddAsync(Appointment appointment);
        Task<List<Appointment>> GetByPatientId(Guid patientId);
        Task<List<Appointment>> GetByDoctorId(Guid doctorId);

    }
}
