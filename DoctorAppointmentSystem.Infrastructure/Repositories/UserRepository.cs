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
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext Context;
        public UserRepository(AppDbContext context)
        {
            Context = context;
        }
        public async Task AddAsync(User user)
        {
            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await Context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
