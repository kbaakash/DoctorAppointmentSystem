using DoctorAppointmentSystem.Domain.Entities;

namespace DoctorAppointmentSystem.Application.Interfaces
{
    public interface ISlotRepository
    {
        Task<List<Slot>> GetAllSlots();
        Task<Slot> GetByIdAsync(int slotId);
    }
}
