using DoctorAppointmentSystem.Application.DTOs;
using DoctorAppointmentSystem.Application.Interfaces;
using DoctorAppointmentSystem.Domain.Entities;
using DoctorAppointmentSystem.Domain.Enums;
namespace DoctorAppointmentSystem.Application.Services
{
    public class AppointmentService:IAppointmentService
    {
        private readonly IDoctorRepository DoctorRepository;
        private readonly ISlotRepository SlotRepository;
        private readonly IAppointmentRepository AppointmentRepository;
        private readonly IPatientRepository PatientRepository;

        public AppointmentService(
            IDoctorRepository doctorRepository,
            ISlotRepository slotRepository,
            IAppointmentRepository appointmentRepository,
            IPatientRepository patientRepository)
        {
            DoctorRepository = doctorRepository;
            SlotRepository = slotRepository;
            AppointmentRepository = appointmentRepository;
            PatientRepository= patientRepository;
        }
        public async Task BookAppointment(Guid userId, CreateAppointmentDto dto)
        {

            // validateing date
            if(dto.Date.Date<DateTime.Today)
                throw new Exception("Invalid date");

            // checking doctor exists
            var doctor = await DoctorRepository.GetByIdAsync(dto.DoctorId);
            if (doctor == null)
                throw new Exception("Doctor not found");

            //checking slot exists
            var slot = await SlotRepository.GetByIdAsync(dto.SlotId);
            if (slot == null)
                throw new Exception("Slot not found");

            //validating slot with doctor working hours
            if (slot.StartTime < doctor.StartTime || slot.StartTime >= doctor.EndTime)
                throw new Exception("Slot outside doctor working hours");

            //checking if slot is already booked
            var isBooked= await AppointmentRepository.IsSlotBooked(dto.DoctorId, dto.Date, dto.SlotId);
            if (isBooked)
                throw new Exception("Slot already booked");
            var patientId = await PatientRepository.GetByUserId(userId);
            //create appointment
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                DoctorId = dto.DoctorId,
                PatientId = patientId.Id,
                Date = dto.Date.Date,
                SlotId = dto.SlotId,
                Status=AppointmentStatus.Booked
            };
            await AppointmentRepository.AddAsync(appointment);
        }
        public async Task<List<AppointmentDto>> GetPatientAppointments(Guid patientId)
        {
            var appointments = await AppointmentRepository.GetByPatientId(patientId);
            return appointments.Select(a => new AppointmentDto
            {
                AppointmentId = a.Id,
                Date = a.Date,
                Slot = a.Slot.Label,
                DoctorName = a.Doctor.Name,
                PatientName = a.Patient.Name,
                Status = a.Status.ToString()
            }).ToList();
        }
        public async Task<List<AppointmentDto>> GetDoctorAppointments(Guid doctorId)
        {
            var appointments = await AppointmentRepository.GetByDoctorId(doctorId);
            return appointments.Select(a => new AppointmentDto
            {
                AppointmentId = a.Id,
                Date = a.Date,
                Slot = a.Slot.Label,
                DoctorName = a.Doctor.Name,
                PatientName = a.Patient.Name,
                Status = a.Status.ToString()
            }).ToList();
        }
    }
}
