using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Database;

namespace SchedulerDBManager.DataAccess.Repositories.Access
{
    public class AccessScheduleRepository : IScheduleRepository
    {
        private readonly IDatabase db;

        public AccessScheduleRepository(IDatabase db)
        {
            this.db = db;
        }

        public IEnumerable<Schedule> GetAll()
        {
            string sql = @"SELECT s.*, sec.address 
                   FROM schedule s 
                   INNER JOIN sections sec ON s.section_id = sec.section_id";
            var dataTable = db.ExecuteSelect(sql);
            return MapToDomain(dataTable);
        }

        public IEnumerable<Schedule> SearchBySupervisor(string name)
        {
            string sql = "SELECT * FROM schedule WHERE supervisor_fullname LIKE ?";
            var param = new OleDbParameter("@p1", $"%{name}%"); // Безопасный параметр
            var dataTable = db.ExecuteSelect(sql, param);
            return MapToDomain(dataTable);
        }

        public void Add(Schedule s)
        {
            string sql = "INSERT INTO schedule (start_time, end_time, duration, worker_count, supervisor_fullname, section_id, shift_date) " +
                         "VALUES (?, ?, ?, ?, ?, ?, ?)";

            db.ExecuteNonQuery(sql,
                new OleDbParameter("@p1", OleDbType.Date) { Value = s.StartTime },
                new OleDbParameter("@p2", OleDbType.Date) { Value = s.EndTime },
                new OleDbParameter("@p3", OleDbType.Integer) { Value = s.Duration },
                new OleDbParameter("@p4", OleDbType.Integer) { Value = s.WorkerCount },
                new OleDbParameter("@p5", OleDbType.VarWChar) { Value = (object)s.SupervisorFullname ?? DBNull.Value },
                new OleDbParameter("@p6", OleDbType.Integer) { Value = s.SectionId },
                new OleDbParameter("@p7", OleDbType.Date) { Value = s.ShiftDate }
            );
        }

        public void Update(Schedule s)
        {
            string sql = $"UPDATE schedule SET start_time=?, end_time=?, duration=?, worker_count=?, supervisor_fullname=?, section_id=?, shift_date=? WHERE shift_id=?";

            db.ExecuteNonQuery(sql,
                new OleDbParameter("@p1", OleDbType.Date) { Value = s.StartTime },
                new OleDbParameter("@p2", OleDbType.Date) { Value = s.EndTime },
                new OleDbParameter("@p3", OleDbType.Integer) { Value = s.Duration },
                new OleDbParameter("@p4", OleDbType.Integer) { Value = s.WorkerCount },
                new OleDbParameter("@p5", OleDbType.VarWChar) { Value = (object)s.SupervisorFullname ?? DBNull.Value },
                new OleDbParameter("@p6", OleDbType.Integer) { Value = s.SectionId },
                new OleDbParameter("@p7", OleDbType.Date) { Value = s.ShiftDate },
                new OleDbParameter("@p8", OleDbType.Integer) { Value = s.ShiftId }
            );
        }

        public void Delete(int id)
        {
            string sql = $"DELETE FROM schedule WHERE shift_id=?";
            db.ExecuteNonQuery(sql, new OleDbParameter("@p1", OleDbType.Integer) { Value = id });
        }

        // Вспомогательный метод (Маппер: таблица -> список объектов)
        private IEnumerable<Schedule> MapToDomain(DataTable dt)
        {
            var list = new List<Schedule>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Schedule
                {
                    ShiftId = Convert.ToInt32(row["shift_id"]),
                    StartTime = Convert.ToDateTime(row["start_time"]),
                    EndTime = Convert.ToDateTime(row["end_time"]),
                    Duration = Convert.ToInt32(row["duration"]),
                    WorkerCount = Convert.ToInt32(row["worker_count"]),
                    SupervisorFullname = row["supervisor_fullname"].ToString(),
                    SectionId = Convert.ToInt32(row["section_id"]),
                    SectionAddress = row["address"]?.ToString().Replace("\"", "").Trim(),
                    ShiftDate = Convert.ToDateTime(row["shift_date"])
                });
            }
            return list;
        }
    }
}