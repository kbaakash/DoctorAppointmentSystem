using DoctorAppointmentSystem.Application.Interfaces;
using DoctorAppointmentSystem.Domain.Entities;
using DoctorAppointmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentSystem.Infrastructure.Repositories
{ 
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext Context;
        public PatientRepository(AppDbContext context)
        {
            Context= context;
        }
        public async Task AddAsync(Patient patient)
        {
                await Context.Patients.AddAsync(patient);
                await Context.SaveChangesAsync();
        }

        public async Task<Patient> GetByUserId(Guid userId)
        {
            return await Context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
