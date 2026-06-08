using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedulerDBManager.BusinessLogic.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository _deptRepo;
        private readonly ISectionRepository _sectionRepo;
        private readonly IScheduleRepository _scheduleRepo;

        // Внедряем смежные репозитории для каскадного удаления
        public DepartmentService(IDepartmentRepository deptRepo, ISectionRepository sectionRepo, IScheduleRepository scheduleRepo)
        {
            _deptRepo = deptRepo;
            _sectionRepo = sectionRepo;
            _scheduleRepo = scheduleRepo;
        }

        public IEnumerable<Department> GetAllDepartments() => _deptRepo.GetAll();

        public void CreateDepartment(Department department)
        {
            Validate(department);
            _deptRepo.Add(department);
        }

        public void UpdateDepartment(Department department)
        {
            Validate(department);
            _deptRepo.Update(department);
        }

        public void RemoveDepartment(int id)
        {
            // Каскадное удаление: Отдел - Участки - Смены
            var sections = _sectionRepo.GetAll().Where(s => s.DepartmentId == id).ToList();
            foreach (var section in sections)
            {
                var schedules = _scheduleRepo.GetAll().Where(sch => sch.SectionId == section.SectionId).ToList();
                foreach (var schedule in schedules)
                {
                    _scheduleRepo.Delete(schedule.ShiftId);
                }
                _sectionRepo.Delete(section.SectionId);
            }
            _deptRepo.Delete(id);
        }

        private void Validate(Department department)
        {
            if (department == null) throw new ArgumentNullException(nameof(department));
            if (string.IsNullOrWhiteSpace(department.DepartmentName))
                throw new ArgumentException("Название подразделения не может быть пустым.");
            if (string.IsNullOrWhiteSpace(department.HeadFullName))
                throw new ArgumentException("ФИО руководителя не может быть пустым.");

            department.DepartmentName = department.DepartmentName.Trim();
            department.HeadFullName = department.HeadFullName?.Trim();
        }
    }
}