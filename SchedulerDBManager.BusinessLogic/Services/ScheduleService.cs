using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerDBManager.BusinessLogic.Services
{
    public class ScheduleService
    {
        private readonly IScheduleRepository repository;


        public ScheduleService(IScheduleRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Schedule> GetAllSchedules()
        {
            return repository.GetAll();
        }

        public IEnumerable<Schedule> FindBySupervisor(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return GetAllSchedules();

            return repository.SearchBySupervisor(name.Trim());
        }

        public void CreateSchedule(Schedule schedule)
        {
            // Если длительность не задана, вычисляем ее
            if (schedule.Duration <= 0)
            {
                schedule.Duration = (int)(schedule.EndTime - schedule.StartTime).TotalHours;
            }

            if (schedule.WorkerCount < 0)
                throw new ArgumentException("Количество рабочих не может быть отрицательным.");

            repository.Add(schedule);
        }

        public void RemoveSchedule(int id)
        {
            repository.Delete(id);
        }

        public void UpdateSchedule(Schedule schedule)
        {
            // Если длительность не задана или изменено время, пересчитываем
            if (schedule.Duration <= 0)
            {
                schedule.Duration = (int)(schedule.EndTime - schedule.StartTime).TotalHours;
            }

            if (schedule.WorkerCount < 0)
                throw new ArgumentException("Количество рабочих не может быть отрицательным.");

            repository.Update(schedule);
        }
    }
}
