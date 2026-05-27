using System.Collections.Generic;
using SchedulerDBManager.DataAccess.Models;

namespace SchedulerDBManager.DataAccess.Repositories
{
    public interface IScheduleRepository
    {
        IEnumerable<Schedule> GetAll();
        IEnumerable<Schedule> SearchBySupervisor(string name);
        void Add(Schedule schedule);
        void Update(Schedule schedule);
        void Delete(int id);
    }
}