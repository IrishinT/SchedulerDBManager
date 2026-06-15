using System;
using System.Collections.Generic;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Repositories;

namespace SchedulerDBManager.BusinessLogic.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        /// <summary>
        /// Получает список всех пользователей системы.
        /// </summary>
        public IEnumerable<User> GetAllUsers() => _userRepo.GetAll();

        /// <summary>
        /// Проводит валидацию пользователя, проверяет уникальность логина и добавляет его в систему.
        /// </summary>
        /// <param name="user">Объект нового пользователя.</param>
        public void CreateUser(User user)
        {
            Validate(user);

            // Проверка на уникальность логина
            if (_userRepo.GetByLogin(user.Login) != null)
                throw new Exception("Пользователь с таким логином уже существует.");

            _userRepo.Add(user);
        }

        /// <summary>
        /// Проверяет и обновляет данные существующей учетной записи.
        /// </summary>
        /// <param name="user">Объект пользователя с обновленными данными.</param>
        public void UpdateUser(User user)
        {
            Validate(user);
            _userRepo.Update(user);
        }

        /// <summary>
        /// Удаляет пользователя из системы по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        public void RemoveUser(int id)
        {
            _userRepo.Delete(id);
        }

        /// <summary>
        /// Выполняет авторизацию пользователя по введенным данным.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Возвращает объект пользователя, если авторизация успешна; иначе - null.</returns>
        public User Authenticate(string login, string password)
        {
            var user = _userRepo.GetByLogin(login);
            if (user != null && user.Password == password)
            {
                return user;
            }
            return null;
        }

        /// <summary>
        /// Внутренний метод для проверки данных учетной записи (наличие логина и пароля, корректность роли, минимальная длина пароля).
        /// </summary>
        /// <param name="user">Модель пользователя.</param>
        private void Validate(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(user.Login))
                throw new ArgumentException("Логин не может быть пустым.");

            if (string.IsNullOrWhiteSpace(user.Password))
                throw new ArgumentException("Пароль не может быть пустым.");

            if (user.Role < 1 || user.Role > 3)
                throw new ArgumentException("Указана некорректная роль пользователя.");

            if (user.Password.Length < 6)
                throw new ArgumentException("Пароль слишком короткий (минимум 6 символов).");



            user.Login = user.Login.Trim();
        }
    }
}