using System.Collections.Generic;
using SchedulerDBManager.DataAccess.Models;

namespace SchedulerDBManager.DataAccess.Repositories
{
    public interface ISectionRepository
    {
        IEnumerable<Section> GetAll();
        IEnumerable<Section> SearchByAddress(string address); // Поиск по адресу
        void Add(Section section);
        void Update(Section section);
        void Delete(int id);
    }
}