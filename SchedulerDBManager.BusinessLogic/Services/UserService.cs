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

        public IEnumerable<User> GetAllUsers() => _userRepo.GetAll();

        public void CreateUser(User user)
        {
            Validate(user);

            // Проверка на уникальность логина
            if (_userRepo.GetByLogin(user.Login) != null)
                throw new Exception("Пользователь с таким логином уже существует.");

            _userRepo.Add(user);
        }

        public void UpdateUser(User user)
        {
            Validate(user);
            _userRepo.Update(user);
        }

        public void RemoveUser(int id)
        {
            _userRepo.Delete(id);
        }

        // Метод для будущей авторизации
        public User Authenticate(string login, string password)
        {
            var user = _userRepo.GetByLogin(login);
            if (user != null && user.Password == password)
            {
                return user;
            }
            return null;
        }

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