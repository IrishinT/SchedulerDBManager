using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerDBManager.DataAccess.Models
{
    public class Schedule
    {
        public int ShiftId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public int WorkerCount { get; set; }
        public string SupervisorFullname { get; set; }
        public int SectionId { get; set; }
        public DateTime ShiftDate { get; set; }
    }
}
