using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Database;

namespace SchedulerDBManager.DataAccess.Repositories.Access
{
    public class AccessUserRepository : IUserRepository
    {
        private readonly IDatabase db;

        public AccessUserRepository(IDatabase db)
        {
            this.db = db;
        }

        public IEnumerable<User> GetAll()
        {
            string sql = "SELECT * FROM users";
            var dataTable = db.ExecuteSelect(sql);
            return MapToDomain(dataTable);
        }

        public User GetByLogin(string login)
        {
            string sql = "SELECT * FROM users WHERE login = ?";
            var param = new OleDbParameter("@p1", OleDbType.VarWChar) { Value = login };
            var result = MapToDomain(db.ExecuteSelect(sql, param));
            return result.FirstOrDefault();
        }

        public void Add(User u)
        {
            // Используем скобки [], так как password и role могут быть зарезервированы
            string sql = "INSERT INTO users (login, [password], [role]) VALUES (?, ?, ?)";

            db.ExecuteNonQuery(sql,
                new OleDbParameter("@p1", OleDbType.VarWChar) { Value = u.Login ?? (object)DBNull.Value },
                new OleDbParameter("@p2", OleDbType.VarWChar) { Value = u.Password ?? (object)DBNull.Value },
                new OleDbParameter("@p3", OleDbType.Integer) { Value = u.Role }
            );
        }

        public void Update(User u)
        {
            string sql = "UPDATE users SET login=?, [password]=?, [role]=? WHERE user_id=?";

            db.ExecuteNonQuery(sql,
                new OleDbParameter("@p1", OleDbType.VarWChar) { Value = u.Login ?? (object)DBNull.Value },
                new OleDbParameter("@p2", OleDbType.VarWChar) { Value = u.Password ?? (object)DBNull.Value },
                new OleDbParameter("@p3", OleDbType.Integer) { Value = u.Role },
                new OleDbParameter("@p4", OleDbType.Integer) { Value = u.UserId }
            );
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM users WHERE user_id=?";
            db.ExecuteNonQuery(sql, new OleDbParameter("@p1", OleDbType.Integer) { Value = id });
        }

        private IEnumerable<User> MapToDomain(DataTable dt)
        {
            var list = new List<User>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new User
                {
                    UserId = Convert.ToInt32(row["user_id"]),
                    Login = row["login"].ToString(),
                    Password = row["password"].ToString(),
                    Role = Convert.ToInt32(row["role"])
                });
            }
            return list;
        }
    }
}