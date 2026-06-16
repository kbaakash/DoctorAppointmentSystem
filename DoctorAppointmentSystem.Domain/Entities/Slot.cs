namespace DoctorAppointmentSystem.Domain.Entities
{
    public class Slot
    {
        public int Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Label { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
