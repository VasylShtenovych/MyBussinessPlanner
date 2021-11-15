using BLL.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> GetAppointmentByIdAsync(int appointmenId);

        Task<AppointmentDto> AddAppointmentAsync(CreateAppointmentDto createAppointmenModel);

        Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync(string userId);

        Task<AppointmentDto> UpdateAppointmentAsync(AppointmentDto appointmentDto);

        Task DeleteAsync(int appointmentId);
    }
}
