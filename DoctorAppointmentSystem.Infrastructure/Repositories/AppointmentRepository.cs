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
    public class AppointmentRepository: IAppointmentRepository
    {
        private readonly AppDbContext Context;
        public AppointmentRepository(AppDbContext context)
        {
            Context = context;
        }
        /// <summary>
        /// Used to get the booked slot ids for a specific doctor on a specific date. This is useful for checking which slots are already booked and which are available when creating or updating an appointment.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<List<int>> GetBookedSlotIds(Guid doctorId, DateTime date)
        {
            return await Context.Appointments
                .Where(x => x.DoctorId == doctorId && x.Date.Date == date.Date)
                .Select(x => x.SlotId)
                .ToListAsync();
        }

        public async Task<bool> IsSlotBooked(Guid doctorId, DateTime date, int slotId)
        {
            return await Context.Appointments
                .AnyAsync(x => x.DoctorId == doctorId && x.Date.Date == date.Date && x.SlotId == slotId);
        }

        public async Task AddAsync(Appointment appointment)
        {
            await Context.Appointments.AddAsync(appointment);
            await Context.SaveChangesAsync();
        }
        public async Task<List<Appointment>> GetByPatientId(Guid patientId)
        {
            return await Context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Slot)
                .Where(a => a.PatientId == patientId)
                .OrderBy(a => a.Date)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetByDoctorId(Guid doctorId)
        {
            return await Context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Slot)
                .Where(a => a.DoctorId == doctorId)
                .OrderBy(a => a.Date)
                .ToListAsync();
        }
    }
}
