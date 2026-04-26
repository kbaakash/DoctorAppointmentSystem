using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Application.DTOs
{
    public class AppointmentDto
    {
        public Guid AppointmentId { get; set; }
        public DateTime Date { get; set; }
        public string Slot { get; set; }

        public string DoctorName { get; set; }   
        public string PatientName { get; set; }

        public string Status { get; set; }
    }
}
