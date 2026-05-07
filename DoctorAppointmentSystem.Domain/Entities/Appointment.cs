using DoctorAppointmentSystem.Domain.Enums;

namespace DoctorAppointmentSystem.Domain.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public DateTime Date { get; set; }
        public int SlotId { get; set; }
        public Slot Slot { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
