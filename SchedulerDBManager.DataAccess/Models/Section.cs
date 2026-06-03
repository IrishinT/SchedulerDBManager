using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerDBManager.DataAccess.Models
{
    public class Section
    {
        public int SectionId { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Phone { get; set; }
    }
}
