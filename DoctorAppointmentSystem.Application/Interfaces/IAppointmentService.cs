using DoctorAppointmentSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Application.Interfaces
{
   public interface IAppointmentService
    {
        Task BookAppointment(Guid patientId, CreateAppointmentDto dto);
        Task<List<AppointmentDto>> GetPatientAppointments(Guid patientId);
        Task<List<AppointmentDto>> GetDoctorAppointments(Guid doctorId);
    }
}
