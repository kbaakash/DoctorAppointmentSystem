using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Application.DTOs
{
    public class CreateAppointmentDto
    {
        [Required]
        public Guid DoctorId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int SlotId { get; set; }
    }
}
