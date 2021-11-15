using AutoMapper;
using BLL.Dto;
using DAL.Entities;

namespace Car.Domain.Mapping
{
    public class AppointmentMapper : Profile
    {
        public AppointmentMapper()
        {
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<Appointment, CreateAppointmentDto>().ReverseMap();
        }
    }
}
