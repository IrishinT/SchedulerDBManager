using System.Collections.Generic;
using SchedulerDBManager.DataAccess.Models;

namespace SchedulerDBManager.DataAccess.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для управления расписанием (рабочими сменами).
    /// </summary>
    public interface IScheduleRepository
    {

        /// <summary>
        /// Получает полный список всех рабочих смен.
        /// </summary>
        IEnumerable<Schedule> GetAll();

        /// <summary>
        /// Ищет смены по частичному совпадению ФИО начальника смены.
        /// </summary>
        /// <param name="name">ФИО начальника или его часть.</param>
        IEnumerable<Schedule> SearchBySupervisor(string name);

        /// <summary>
        /// Создает новую запись о рабочей смене в базе данных.
        /// </summary>
        /// <param name="schedule">Объект рабочей смены.</param>
        void Add(Schedule schedule);

        /// <summary>
        /// Обновляет информацию о существующей рабочей смене.
        /// </summary>
        /// <param name="schedule">Объект смены с обновленными данными.</param>
        void Update(Schedule schedule);

        /// <summary>
        /// Удаляет рабочую смену из расписания по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор смены (ShiftId).</param>
        void Delete(int id);
    }
}