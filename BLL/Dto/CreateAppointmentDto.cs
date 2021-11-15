using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class CreateAppointmentDto
    {
        public DateTime StartTime { get; set; }
        public double Cost { get; set; }
        public string UserId { get; set; }
    }
}
