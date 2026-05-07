using DoctorAppointmentSystem.Application.Interfaces;
using DoctorAppointmentSystem.Domain.Entities;
using DoctorAppointmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentSystem.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext Context;

        public DoctorRepository(AppDbContext context)
        {
            Context = context;
        }

        public async Task AddAsync(Doctor doctor)
        {
            await Context.Doctors.AddAsync(doctor);
            await Context.SaveChangesAsync();
        }

        public async Task<Doctor> GetByIdAsync(Guid id)
        {
            return await Context.Doctors.FindAsync(id);
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            return await Context.Doctors.ToListAsync();
        }
    }
}
