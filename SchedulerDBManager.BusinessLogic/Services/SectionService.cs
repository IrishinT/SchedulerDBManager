using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedulerDBManager.BusinessLogic.Services
{
    public class SectionService
    {
        private readonly ISectionRepository repository;

        public SectionService(ISectionRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Section> GetAllSections()
        {
            return repository.GetAll();
        }

        public IEnumerable<Section> FindByAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return GetAllSections();

            return repository.SearchByAddress(address.Trim());
        }

        public void CreateSection(Section section)
        {
            if (string.IsNullOrWhiteSpace(section.Address))
                throw new ArgumentException("Адрес участка не может быть пустым.");

            repository.Add(section);
        }

        public void UpdateSection(Section section)
        {
            if (string.IsNullOrWhiteSpace(section.Address))
                throw new ArgumentException("Адрес участка не может быть пустым.");

            repository.Update(section);
        }

        public void RemoveSection(int id)
        {
            repository.Delete(id);
        }
    }
}