using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Domain.Enums
{
    public enum AppointmentStatus
    {
        Booked,
        Cancelled,
        Completed,
        NoShow,
        Rescheduled
    }
}
