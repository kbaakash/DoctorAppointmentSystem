using DoctorAppointmentSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Application.Interfaces
{
    public interface IAuthService
    {
         public Task<string> LoginAsync(LoginDto dto);
        public Task RegisterAsync(RegisterPatientDto dto);
    }
}
