using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedulerDBManager.BusinessLogic.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return repository.GetAll();
        }

        public IEnumerable<Department> FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return GetAllDepartments();

            return repository.SearchByName(name.Trim());
        }

        public void CreateDepartment(Department department)
        {
            if (string.IsNullOrWhiteSpace(department.DepartmentName))
                throw new ArgumentException("Название подразделения не может быть пустым.");

            repository.Add(department);
        }

        public void UpdateDepartment(Department department)
        {
            if (string.IsNullOrWhiteSpace(department.DepartmentName))
                throw new ArgumentException("Название подразделения не может быть пустым.");

            repository.Update(department);
        }

        public void RemoveDepartment(int id)
        {
            repository.Delete(id);
        }
    }
}