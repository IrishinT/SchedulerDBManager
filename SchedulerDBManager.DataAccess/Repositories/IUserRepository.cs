using System.Collections.Generic;
using SchedulerDBManager.DataAccess.Models;

namespace SchedulerDBManager.DataAccess.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetByLogin(string login);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
    }
}