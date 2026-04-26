using DoctorAppointmentSystem.Application.Interfaces;
using DoctorAppointmentSystem.Domain.Entities;
using DoctorAppointmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Infrastructure.Repositories
{
    public class SlotRepository : ISlotRepository
    {
        private readonly AppDbContext Context;
        public SlotRepository(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<Slot>> GetAllSlots()
        {
        return await Context.Slots.ToListAsync();
        }
        public async Task<Slot> GetByIdAsync(int slotId)
        {
            return await Context.Slots.FindAsync(slotId);
        }
    }   
}
