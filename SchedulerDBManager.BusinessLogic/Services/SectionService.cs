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

        public IEnumerable<Section> GetAllSections() => _sectionRepo.GetAll();

        public void CreateSection(Section section)
        {
            Validate(section);
            _sectionRepo.Add(section);
        }

        public void UpdateSection(Section section)
        {
            Validate(section);
            _sectionRepo.Update(section);
        }

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