using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Repositories;

namespace SchedulerDBManager.BusinessLogic.Services
{
    public class SectionService
    {
        private readonly ISectionRepository _sectionRepo;
        private readonly IScheduleRepository _scheduleRepo;

        public SectionService(ISectionRepository sectionRepo, IScheduleRepository scheduleRepo)
        {
            _sectionRepo = sectionRepo;
            _scheduleRepo = scheduleRepo;
        }

        /// <summary>
        /// Возвращает список всех производственных участков с привязанными к ним подразделениями.
        /// </summary>
        public IEnumerable<Section> GetAllSections() => _sectionRepo.GetAll();

        /// <summary>
        /// Проводит валидацию данных и создает новый участок в БД.
        /// </summary>
        /// <param name="section">Объект нового участка.</param>
        public void CreateSection(Section section)
        {
            Validate(section);
            _sectionRepo.Add(section);
        }

        /// <summary>
        /// Валидирует измененные данные и обновляет участок в БД.
        /// </summary>
        /// <param name="section">Объект участка с обновленными данными.</param>
        public void UpdateSection(Section section)
        {
            Validate(section);
            _sectionRepo.Update(section);
        }

        /// <summary>
        /// Удаляет участок по идентификатору.
        /// Выполняет каскадное удаление всех смен, которые закреплены за этим участком.
        /// </summary>
        /// <param name="id">Идентификатор участка.</param>
        public void RemoveSection(int id)
        {
            // Каскадное удаление: Участок - Смены
            var schedules = _scheduleRepo.GetAll().Where(s => s.SectionId == id).ToList();
            foreach (var schedule in schedules)
            {
                _scheduleRepo.Delete(schedule.ShiftId);
            }
            _sectionRepo.Delete(id);
        }

        /// <summary>
        /// Внутренний метод для проверки корректности данных участка.
        /// </summary>
        /// <param name="section">Модель участка.</param>
        private void Validate(Section section)
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (string.IsNullOrWhiteSpace(section.Address))
                throw new ArgumentException("Адрес участка не может быть пустым.");
            if (section.DepartmentId <= 0)
                throw new ArgumentException("Участок должен быть привязан к подразделению.");

            section.Address = section.Address.Trim();
            section.Phone = section.Phone?.Trim();
        }
    }
}