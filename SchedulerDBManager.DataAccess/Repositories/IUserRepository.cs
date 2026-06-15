using System.Collections.Generic;
using SchedulerDBManager.DataAccess.Models;

namespace SchedulerDBManager.DataAccess.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для управления учетными записями пользователей.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Получает список всех пользователей системы.
        /// </summary>
        IEnumerable<User> GetAll();

        /// <summary>
        /// Ищет пользователя по точному совпадению логина (для авторизации и проверки уникальности).
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <returns>Объект User, если найден, иначе null.</returns>
        User GetByLogin(string login);

        /// <summary>
        /// Создает новую учетную запись пользователя.
        /// </summary>
        /// <param name="user">Объект нового пользователя.</param>
        void Add(User user);

        /// <summary>
        /// Обновляет логин, пароль или роль существующего пользователя.
        /// </summary>
        /// <param name="user">Объект пользователя с обновленными данными.</param>
        void Update(User user);

        /// <summary>
        /// Удаляет учетную запись пользователя по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        void Delete(int id);
    }
}