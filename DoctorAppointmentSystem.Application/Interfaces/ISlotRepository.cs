using DoctorAppointmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Application.Interfaces
{
    public interface ISlotRepository
    {
        Task<List<Slot>> GetAllSlots();
        Task<Slot> GetByIdAsync(int slotId);
    }
}
