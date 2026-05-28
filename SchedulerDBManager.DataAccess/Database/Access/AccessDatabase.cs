using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;

namespace SchedulerDBManager.DataAccess.Database.Access
{
    public class AccessDatabase : IDatabase
    {

        private readonly string connectionString;

        public AccessDatabase(string databasePath = "ShiftSchedule.accdb")
        {
            if (string.IsNullOrWhiteSpace(databasePath))
                throw new ArgumentException("Путь к файлу базы данных не может быть пустым", nameof(databasePath));

            connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath};";
        }

        public DataTable ExecuteSelect(string query, params OleDbParameter[] parameters)
        {
            ValidateQuery(query);

            using (var conn = GetConnection())
            using (var cmd = GetCommand(query, conn, parameters))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                var dt = new DataTable();
                new OleDbDataAdapter(cmd).Fill(dt);
                return dt;
            }
        }

        public void ExecuteNonQuery(string query, params OleDbParameter[] parameters)
        {
            ValidateQuery(query);

            using (var conn = GetConnection())
            using (var cmd = GetCommand(query, conn, parameters))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private OleDbConnection GetConnection()
        {
            return new OleDbConnection(connectionString);
        }

        private OleDbCommand GetCommand(string query, OleDbConnection connection, params OleDbParameter[] parameters)
        {
            var cmd = new OleDbCommand(query, connection);
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            return cmd;
        }

        private void ValidateQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Запрос не может быть пустым", nameof(query));
        }
    }
}