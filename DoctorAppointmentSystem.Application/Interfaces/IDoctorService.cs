using DoctorAppointmentSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Application.Interfaces
{
    public interface IDoctorService
    {
        Task CreateDoctorAsync(CreateDoctorDto dto);
        Task<List<SlotDto>> GetAvailableSlots(Guid doctorId, DateTime date);
    }
}
