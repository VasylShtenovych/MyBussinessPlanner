using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Appointment : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public double Cost { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
