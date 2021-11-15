using AutoMapper;
using BLL.Dto;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<Appointment> _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IRepository<Appointment> appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<AppointmentDto> AddAppointmentAsync(CreateAppointmentDto createAppointmenModel)
        {
            var appointment = _mapper.Map<CreateAppointmentDto, Appointment>(createAppointmenModel);
            var newAppointment = await _appointmentRepository.AddAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();
            return _mapper.Map<Appointment, AppointmentDto>(newAppointment);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync(string userId)
        {
            var appointments = await _appointmentRepository
                .Query()
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<AppointmentDto> GetAppointmentByIdAsync(int appointmenId)
        {
            var appointment = await _appointmentRepository
                .Query()
                .FirstOrDefaultAsync(a => a.Id == appointmenId);

            return _mapper.Map<Appointment, AppointmentDto>(appointment);
        }

        public async Task DeleteAsync(int appointmentId)
        {
            var appoitment = await _appointmentRepository.GetByIdAsync(appointmentId);


            _appointmentRepository.Delete(appoitment);
            await _appointmentRepository.SaveChangesAsync();
        }

        public async Task<AppointmentDto> UpdateAppointmentAsync(AppointmentDto appointmentDto)
        {
            var appoitment = await _appointmentRepository.GetByIdAsync(appointmentDto.Id);
            appoitment.Cost = appointmentDto.Cost;
            appoitment.StartTime = appointmentDto.StartTime;
            appoitment.UserId = appointmentDto.UserId;

            await _appointmentRepository.SaveChangesAsync();
            return _mapper.Map<Appointment, AppointmentDto>(appoitment);
        }
    }
}
