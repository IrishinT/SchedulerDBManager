using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Database;

namespace SchedulerDBManager.DataAccess.Repositories.Access
{
    public class AccessDepartmentRepository : IDepartmentRepository
    {
        private readonly IDatabase db;

        public AccessDepartmentRepository(IDatabase db)
        {
            this.db = db;
        }

        public IEnumerable<Department> GetAll()
        {
            string sql = "SELECT * FROM department";
            var dataTable = db.ExecuteSelect(sql);
            return MapToDomain(dataTable);
        }

        public IEnumerable<Department> SearchByName(string name)
        {
            string sql = "SELECT * FROM department WHERE department_name LIKE ?";
            var param = new OleDbParameter("@p1", OleDbType.VarWChar) { Value = $"%{name}%" };
            return MapToDomain(db.ExecuteSelect(sql, param));
        }

        public void Add(Department d)
        {
            string sql = "INSERT INTO department (department_name, head_fullname) VALUES (?, ?)";

            db.ExecuteNonQuery(sql,
                new OleDbParameter("@p1", OleDbType.VarWChar) { Value = d.DepartmentName ?? (object)DBNull.Value },
                new OleDbParameter("@p2", OleDbType.VarWChar) { Value = d.HeadFullName ?? (object)DBNull.Value }
            );
        }

        public void Update(Department d)
        {
            string sql = "UPDATE department SET department_name=?, head_fullname=? WHERE department_id=?";

            db.ExecuteNonQuery(sql,
                new OleDbParameter("@p1", OleDbType.VarWChar) { Value = d.DepartmentName ?? (object)DBNull.Value },
                new OleDbParameter("@p2", OleDbType.VarWChar) { Value = d.HeadFullName ?? (object)DBNull.Value },
                new OleDbParameter("@p3", OleDbType.Integer) { Value = d.DepartmentId }
            );
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM department WHERE department_id=?";
            db.ExecuteNonQuery(sql, new OleDbParameter("@p1", OleDbType.Integer) { Value = id });
        }

        private IEnumerable<Department> MapToDomain(DataTable dt)
        {
            var list = new List<Department>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Department
                {
                    DepartmentId = Convert.ToInt32(row["department_id"]),
                    DepartmentName = row["department_name"].ToString(),
                    HeadFullName = row["head_fullname"].ToString()
                });
            }
            return list;
        }
    }
}