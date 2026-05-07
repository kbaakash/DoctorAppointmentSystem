using DoctorAppointmentSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentSystem.Domain.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
