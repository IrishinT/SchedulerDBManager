using System.Collections.Generic;
using SchedulerDBManager.DataAccess.Models;

namespace SchedulerDBManager.DataAccess.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для управления производственными участками.
    /// </summary>
    public interface ISectionRepository
    {
        /// <summary>
        /// Получает список всех участков.
        /// </summary>
        IEnumerable<Section> GetAll();

        /// <summary>
        /// Ищет производственные участки по частичному совпадению адреса.
        /// </summary>
        /// <param name="address">Искомый адрес или его часть.</param>
        IEnumerable<Section> SearchByAddress(string address); // Поиск по адресу

        /// <summary>
        /// Добавляет новый производственный участок.
        /// </summary>
        /// <param name="section">Объект участка.</param>
        void Add(Section section);

        /// <summary>
        /// Обновляет данные производственного участка.
        /// </summary>
        /// <param name="section">Объект участка с новыми данными.</param>
        void Update(Section section);

        /// <summary>
        /// Удаляет участок по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор участка.</param>
        void Delete(int id);
    }
}