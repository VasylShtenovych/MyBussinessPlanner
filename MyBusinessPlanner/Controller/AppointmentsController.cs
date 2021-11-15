using BLL.Dto;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id) =>
            Ok(await _appointmentService.GetAppointmentByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromBody] CreateAppointmentDto appointmentDto)
        {
            return Ok(await _appointmentService.AddAppointmentAsync(appointmentDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment([FromBody] AppointmentDto appointmentDto)
        {
            return Ok(await _appointmentService.UpdateAppointmentAsync(appointmentDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _appointmentService.DeleteAsync(id);
            return Ok();
        }
    }
}
