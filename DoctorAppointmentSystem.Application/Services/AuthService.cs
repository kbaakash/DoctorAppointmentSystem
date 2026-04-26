using DoctorAppointmentSystem.Application.DTOs;
using DoctorAppointmentSystem.Application.Interfaces;
using DoctorAppointmentSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;


namespace DoctorAppointmentSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDoctorRepository DoctorRepository;
        private readonly IJwtService JwtService;
        private readonly IPasswordHasher<User> PasswordHasher;
        private readonly IPatientRepository PatientRepository;
        private readonly IUserRepository UserRepository;
        public AuthService(IDoctorRepository doctorRepository, IPasswordHasher<User> passwordHasher, IJwtService jwtService, IPatientRepository patientRepository, IUserRepository userRepository)
        {
            DoctorRepository = doctorRepository;
            PasswordHasher = passwordHasher;
            JwtService = jwtService;
            PatientRepository = patientRepository;
            UserRepository = userRepository;
        }
        public async Task<string> LoginAsync(LoginDto dto)
        {
            // For doctor logins
            var user = await UserRepository.GetByEmailAsync(dto.Email);
            if (user != null)
            {
                var result = PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    return JwtService.GenerateToken(user.Id, dto.Email, user.Role.ToString());
                }
            }
            throw new UnauthorizedAccessException("Invalid email or password");
        }
        public async Task RegisterAsync(RegisterPatientDto dto)
        {
            var existing= await UserRepository.GetByEmailAsync(dto.Email);
            if(existing != null) {
                throw new Exception("Email already exists");
            }
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                Role = Domain.Enums.UserRole.Patient
            };
            user.PasswordHash = PasswordHasher.HashPassword(user, dto.Password);
            await UserRepository.AddAsync(user);
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                UserId = user.Id

            };
            await PatientRepository.AddAsync(patient);
        }
    }
}
