using SchedulerDBManager.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerDBManager.DataAccess.Repositories
{
    public interface IDepartmentRepository
    {

        IEnumerable<Department> GetAll();
        IEnumerable<Department> SearchByName(string name);
        void Add(Department schedule);
        void Update(Department schedule);
        void Delete(int id);

    }
}
