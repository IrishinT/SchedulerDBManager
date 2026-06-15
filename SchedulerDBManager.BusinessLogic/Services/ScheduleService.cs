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

        /// <summary>
        /// Получает полный список всех запланированных рабочих смен.
        /// </summary>
        public IEnumerable<Schedule> GetAllSchedules() => _scheduleRepo.GetAll();


        /// <summary>
        /// Проверяет данные смены, производит внутренние расчеты (длительность) и сохраняет ее в базу данных.
        /// </summary>
        /// <param name="schedule">Объект создаваемой смены.</param>
        public void CreateSchedule(Schedule schedule)
        {
            ValidateAndPrepare(schedule);
            _scheduleRepo.Add(schedule);
        }

        /// <summary>
        /// Проверяет данные и обновляет существующую запись о смене в БД.
        /// </summary>
        /// <param name="schedule">Объект смены с обновленными данными.</param>
        public void UpdateSchedule(Schedule schedule)
        {
            ValidateAndPrepare(schedule);
            _scheduleRepo.Update(schedule);
        }

        /// <summary>
        /// Удаляет смену по ее уникальному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой смены.</param>
        public void RemoveSchedule(int id)
        {
            _scheduleRepo.Delete(id);
        }

        /// <summary>
        /// Внутренний метод валидации и подготовки данных смены: 
        /// проверка дат, количества рабочих и автоматический расчет длительности.
        /// </summary>
        /// <param name="schedule">Модель смены.</param>
        private void ValidateAndPrepare(Schedule schedule)
        {
            if (schedule == null) throw new ArgumentNullException(nameof(schedule));

            if (schedule.StartTime >= schedule.EndTime)
                throw new ArgumentException("Время начала смены должно быть раньше времени окончания.");

            if (schedule.WorkerCount <= 0)
                throw new ArgumentException("Количество рабочих должно быть больше нуля.");

            if (schedule.WorkerCount > 100)
                throw new ArgumentException("Количество рабочих должно быть больше 100.");

            if (string.IsNullOrWhiteSpace(schedule.SupervisorFullname))
                throw new ArgumentException("Укажите ФИО начальника смены.");

            // Инкапсулируем бизнес-логику расчетов внутри сервиса
            schedule.Duration = (int)(schedule.EndTime - schedule.StartTime).TotalHours;
            schedule.ShiftDate = schedule.StartTime.Date;
            schedule.SupervisorFullname = schedule.SupervisorFullname.Trim();
        }
    }
}