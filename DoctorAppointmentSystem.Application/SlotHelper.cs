
namespace DoctorAppointmentSystem.Application
{
    public static class SlotHelper
    {
        public static List<string> GenerateSlots(TimeSpan start, TimeSpan end, int durationMinutes)
        {
            var slots = new List<string>();
            var current = start;
            while (current < end)
            {
                var next = current.Add(TimeSpan.FromMinutes(durationMinutes));
                slots.Add($"{current:hh\\:mm}-{next:hh\\:mm}");
                current = next;
            }
            return slots;
        }
    }
}
