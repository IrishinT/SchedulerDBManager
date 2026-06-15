using SchedulerDBManager.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerDBManager.DataAccess.Repositories
{

    /// <summary>
    /// Интерфейс репозитория для управления данными подразделений.
    /// </summary>
    public interface IDepartmentRepository
    {

        /// <summary>
        /// Получает список всех подразделений из базы данных.
        /// </summary>
        IEnumerable<Department> GetAll();

        /// <summary>
        /// Выполняет поиск подразделений по частичному совпадению названия.
        /// </summary>
        /// <param name="name">Искомое название или его часть.</param>
        IEnumerable<Department> SearchByName(string name);

        /// <summary>
        /// Добавляет новое подразделение в базу данных.
        /// </summary>
        /// <param name="department">Объект подразделения для добавления.</param>
        void Add(Department schedule);

        /// <summary>
        /// Обновляет данные существующего подразделения.
        /// </summary>
        /// <param name="department">Объект подразделения с новыми данными.</param>
        void Update(Department schedule);

        /// <summary>
        /// Удаляет подразделение по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого подразделения.</param>
        void Delete(int id);

    }
}
