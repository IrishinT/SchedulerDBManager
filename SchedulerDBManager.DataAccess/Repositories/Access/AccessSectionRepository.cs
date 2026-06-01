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
            string sql = "SELECT * FROM sections";
            var dataTable = db.ExecuteSelect(sql);
            return MapToDomain(dataTable);
        }

        public IEnumerable<Section> SearchByAddress(string address)
        {
            string sql = "SELECT * FROM sections WHERE address LIKE ?";
            var param = new OleDbParameter("@p1", OleDbType.LongVarWChar) { Value = $"%{address}%" };
            return MapToDomain(db.ExecuteSelect(sql, param));
        }

        public void Add(Section s)
        {
            string sql = "INSERT INTO sections (address, department_id, phone) VALUES (?, ?, ?)";

            db.ExecuteNonQuery(sql,
                new OleDbParameter("@p1", OleDbType.LongVarWChar) { Value = s.Address ?? (object)DBNull.Value },
                new OleDbParameter("@p2", OleDbType.Integer) { Value = Convert.ToInt32(s.DepartmentId) },
                new OleDbParameter("@p3", OleDbType.VarWChar) { Value = s.Phone ?? (object)DBNull.Value }
            );
        }

        public void Update(Section s)
        {
            string sql = $"UPDATE sections SET address=?, department_id=?, phone=? WHERE section_id={s.SectionId}";

            db.ExecuteNonQuery(sql,
                new OleDbParameter("@p1", OleDbType.LongVarWChar) { Value = s.Address ?? (object)DBNull.Value },
                new OleDbParameter("@p2", OleDbType.Integer) { Value = Convert.ToInt32(s.DepartmentId) },
                new OleDbParameter("@p3", OleDbType.VarWChar) { Value = s.Phone ?? (object)DBNull.Value }
            );
        }

        public void Delete(int id)
        {
            string sql = $"DELETE FROM sections WHERE section_id={id}";
            db.ExecuteNonQuery(sql);
        }

        // Вспомогательный метод для маппинга данных
        private IEnumerable<Section> MapToDomain(DataTable dt)
        {
            var list = new List<Section>();
            foreach (DataRow row in dt.Rows)
            {
                // Очищаем адрес от случайных кавычек из БД (например " Москва, ул...")
                string rawAddress = row["address"].ToString().Replace("\"", "").Trim();

                list.Add(new Section
                {
                    SectionId = Convert.ToInt32(row["section_id"]),
                    Address = rawAddress,
                    DepartmentId = row["department_id"].ToString(),
                    Phone = row["phone"].ToString()
                });
            }
            return list;
        }
    }
}