using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;

namespace SchedulerDBManager.DataAccess.Database.Access
{
    public class AccessDatabase : IDatabase
    {
        private readonly string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ShiftSchedule.accdb;";

        public DataTable ExecuteSelect(string query, params OleDbParameter[] parameters)
        {
            using (var conn = new OleDbConnection(connectionString))
            using (var cmd = new OleDbCommand(query, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                var dt = new DataTable();
                new OleDbDataAdapter(cmd).Fill(dt);
                return dt;
            }
        }

        public void ExecuteNonQuery(string query, params OleDbParameter[] parameters)
        {
            using (var conn = new OleDbConnection(connectionString))
            using (var cmd = new OleDbCommand(query, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}