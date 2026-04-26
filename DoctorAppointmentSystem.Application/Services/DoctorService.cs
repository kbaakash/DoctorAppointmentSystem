using DoctorAppointmentSystem.Application.DTOs;
using DoctorAppointmentSystem.Application.Interfaces;
using DoctorAppointmentSystem.Domain.Entities;
using DoctorAppointmentSystem.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository DoctorRepository;
        private readonly IUserRepository UserRepository;
        private readonly IPasswordHasher<User> PasswordHasher;
        private readonly ISlotRepository SlotRepository;
        private readonly IAppointmentRepository AppointmentRepository;

        public DoctorService(IDoctorRepository doctorRepository, IUserRepository userRepository, IPasswordHasher<User> passwordHasher, ISlotRepository slotRepository, IAppointmentRepository appointmentRepository)
        {
            DoctorRepository = doctorRepository;
            UserRepository = userRepository;
            PasswordHasher = passwordHasher;
            SlotRepository = slotRepository;
            AppointmentRepository = appointmentRepository;
        }
        /// <summary>
        /// Used to create a new doctor in the system. It checks if a doctor with the same email already exists and throws an exception if it does. If not, it creates a new doctor entity, hashes the password, and saves it to the database.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task CreateDoctorAsync(CreateDoctorDto dto)
        {
            var existingUser = await UserRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new Exception("A user with this email already exists.");
            }

            // 2. Create User (Auth data)
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                Role = UserRole.Doctor
            };

            user.PasswordHash = PasswordHasher.HashPassword(user, dto.Password);

            await UserRepository.AddAsync(user);

            // 3. Create Doctor 
            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Name = dto.Name,
                Specialization = dto.Specialization,
                HospitalName = dto.HospitalName,
                Address = dto.Address,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            await DoctorRepository.AddAsync(doctor);
        
        }

        public async Task<List<SlotDto>> GetAvailableSlots(Guid doctorId, DateTime date)
        {
            var doctor = await DoctorRepository.GetByIdAsync(doctorId);

            if (doctor == null)
                throw new Exception("Doctor not found");
            // geting all 48 slots
            var allSlots = await SlotRepository.GetAllSlots();

            //Filter slots by doctor working hours
            var validSlots = allSlots.Where(s => s.StartTime >= doctor.StartTime && s.EndTime <=doctor.EndTime).ToList();

            // Booked slots
            var bookedSlotIds = await AppointmentRepository.GetBookedSlotIds(doctorId, date);

            // removing booked slots from valid slots
            var availableSlots = validSlots.Where(s => !bookedSlotIds.Contains(s.Id)).Select(s => new SlotDto
            {
             SlotId = s.Id,
              Label = s.Label
            })
       .ToList();
            return availableSlots;
        }
    }
}
