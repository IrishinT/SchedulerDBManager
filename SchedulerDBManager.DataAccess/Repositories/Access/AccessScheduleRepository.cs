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
            string sql = "SELECT * FROM schedule";
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
            // Явный, жестко заданный SQL. Никакого мусора и динамической генерации.
            string sql = "INSERT INTO schedule (start_time, end_time, duration, worker_count, supervisor_fullname, section_id, shift_date) " +
                         "VALUES (?, ?, ?, ?, ?, ?, ?)";

            db.ExecuteNonQuery(sql,
                new OleDbParameter("@p1", s.StartTime),
                new OleDbParameter("@p2", s.EndTime),
                new OleDbParameter("@p3", s.Duration),
                new OleDbParameter("@p4", s.WorkerCount),
                new OleDbParameter("@p5", s.SupervisorFullname),
                new OleDbParameter("@p6", s.SectionId),
                new OleDbParameter("@p7", s.ShiftDate)
            );
        }

        public void Update(Schedule s)
        {
            string sql = "UPDATE schedule SET start_time=?, end_time=?, duration=?, worker_count=?, supervisor_fullname=?, section_id=?, shift_date=? WHERE shift_id=?";
            db.ExecuteNonQuery(sql,
                new OleDbParameter("@p1", s.StartTime),
                new OleDbParameter("@p2", s.EndTime),
                new OleDbParameter("@p3", s.Duration),
                new OleDbParameter("@p4", s.WorkerCount),
                new OleDbParameter("@p5", s.SupervisorFullname),
                new OleDbParameter("@p6", s.SectionId),
                new OleDbParameter("@p7", s.ShiftDate),
                new OleDbParameter("@p8", s.ShiftId) // ID всегда последний в UPDATE
            );
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM schedule WHERE shift_id=?";
            db.ExecuteNonQuery(sql, new OleDbParameter("@p1", id));
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
                    Duration = Convert.ToDouble(row["duration"]),
                    WorkerCount = Convert.ToInt32(row["worker_count"]),
                    SupervisorFullname = row["supervisor_fullname"].ToString(),
                    SectionId = Convert.ToInt32(row["section_id"]),
                    ShiftDate = Convert.ToDateTime(row["shift_date"])
                });
            }
            return list;
        }
    }
}