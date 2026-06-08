using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Repositories;

namespace SchedulerDBManager.BusinessLogic.Services
{
    public class ScheduleService
    {
        private readonly IScheduleRepository _scheduleRepo;

        public ScheduleService(IScheduleRepository scheduleRepo)
        {
            _scheduleRepo = scheduleRepo;
        }

        public IEnumerable<Schedule> GetAllSchedules() => _scheduleRepo.GetAll();

        public void CreateSchedule(Schedule schedule)
        {
            ValidateAndPrepare(schedule);
            _scheduleRepo.Add(schedule);
        }

        public void UpdateSchedule(Schedule schedule)
        {
            ValidateAndPrepare(schedule);
            _scheduleRepo.Update(schedule);
        }

        public void RemoveSchedule(int id)
        {
            _scheduleRepo.Delete(id);
        }

        private void ValidateAndPrepare(Schedule schedule)
        {
            if (schedule == null) throw new ArgumentNullException(nameof(schedule));

            if (schedule.StartTime >= schedule.EndTime)
                throw new ArgumentException("Время начала смены должно быть раньше времени окончания.");

            if (schedule.WorkerCount <= 0)
                throw new ArgumentException("Количество рабочих должно быть больше нуля.");

            if (string.IsNullOrWhiteSpace(schedule.SupervisorFullname))
                throw new ArgumentException("Укажите ФИО начальника смены.");

            // Инкапсулируем бизнес-логику расчетов внутри сервиса
            schedule.Duration = (int)(schedule.EndTime - schedule.StartTime).TotalHours;
            schedule.ShiftDate = schedule.StartTime.Date;
            schedule.SupervisorFullname = schedule.SupervisorFullname.Trim();
        }
    }
}