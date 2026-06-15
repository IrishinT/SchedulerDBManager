using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Database;

namespace SchedulerDBManager.DataAccess.Repositories.Access
{
    public class AccessSectionRepository : ISectionRepository
    {
        private readonly IDatabase db;

        public AccessSectionRepository(IDatabase db)
        {
            this.db = db;
        }

        public IEnumerable<Section> GetAll()
        {
            string sql = @"SELECT sec.*, dep.department_name 
                   FROM sections sec 
                   INNER JOIN department dep ON sec.department_id = dep.department_id";
            var dataTable = db.ExecuteSelect(sql);
            return MapToDomain(dataTable);
        }

        public IEnumerable<Section> SearchByAddress(string address)
        {
            string sql = @"SELECT sec.*, dep.department_name 
                   FROM sections sec 
                   INNER JOIN department dep ON sec.department_id = dep.department_id 
                   WHERE sec.address LIKE ?";
            var param = new OleDbParameter("@p1", OleDbType.LongVarWChar) { Value = $"%{address}%" };
            return MapToDomain(db.ExecuteSelect(sql, param));
        }

        public void Add(Section s)
        {
            string sql = "INSERT INTO sections (address, department_id, phone) VALUES (?, ?, ?)";

            db.ExecuteNonQuery(sql,
                new OleDbParameter("@p1", OleDbType.LongVarWChar) { Value = s.Address ?? (object)DBNull.Value },
                new OleDbParameter("@p2", OleDbType.Integer) { Value = s.DepartmentId },
                new OleDbParameter("@p3", OleDbType.VarWChar) { Value = s.Phone ?? (object)DBNull.Value }
            );
        }

        public void Update(Section s)
        {
            string sql = $"UPDATE sections SET address=?, department_id=?, phone=? WHERE section_id=?";

            db.ExecuteNonQuery(sql,
                new OleDbParameter("@p1", OleDbType.LongVarWChar) { Value = s.Address ?? (object)DBNull.Value },
                new OleDbParameter("@p2", OleDbType.Integer) { Value = s.DepartmentId },
                new OleDbParameter("@p3", OleDbType.VarWChar) { Value = s.Phone ?? (object)DBNull.Value },
                new OleDbParameter("@p4", OleDbType.Integer) { Value = s.SectionId }
            );
        }

        public void Delete(int id)
        {
            string sql = $"DELETE FROM sections WHERE section_id=?";
            db.ExecuteNonQuery(sql, new OleDbParameter("@p1", OleDbType.Integer) { Value = id });
        }

        // Вспомогательный метод для маппинга данных
        private IEnumerable<Section> MapToDomain(DataTable dt)
        {
            var list = new List<Section>();
            foreach (DataRow row in dt.Rows)
            {
                // Очищаем адрес от случайных кавычек из БД
                string rawAddress = row["address"].ToString().Replace("\"", "").Trim();

                list.Add(new Section
                {
                    SectionId = Convert.ToInt32(row["section_id"]),
                    Address = rawAddress,
                    DepartmentId = Convert.ToInt32(row["department_id"]),
                    DepartmentName = row["department_name"].ToString(),
                    Phone = row["phone"].ToString()
                });
            }
            return list;
        }
    }
}