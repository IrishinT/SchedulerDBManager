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

        public void CheckConnection()
        {
           using (var conn = GetConnection())
           {
              conn.Open();
              conn.Close();
           }
        }

        public DataTable ExecuteSelect(string query, params OleDbParameter[] parameters)
        {
            ValidateQuery(query);

            DataTable dt = new DataTable();

            using (var connection = new OleDbConnection(connectionString))
            using (var command = new OleDbCommand(query, connection))
            {
                if (parameters != null && parameters.Length > 0)
                {
                    foreach (var p in parameters)
                    {
                        command.Parameters.Add(p);
                    }
                }

                using (var adapter = new OleDbDataAdapter(command))
                {
                    adapter.Fill(dt);
                }

                command.Parameters.Clear();
            }
            return dt;
        }

        public void ExecuteNonQuery(string query, params OleDbParameter[] parameters)
        {
            ValidateQuery(query);

            using (var connection = new OleDbConnection(connectionString))
            using (var command = new OleDbCommand(query, connection))
            {
                if (parameters != null && parameters.Length > 0)
                {
                    foreach (var p in parameters)
                    {
                        command.Parameters.Add(p);
                    }
                }

                connection.Open();
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
        }

        private OleDbConnection GetConnection()
        {
            return new OleDbConnection(connectionString);
        }

        private void ValidateQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Запрос не может быть пустым", nameof(query));
        }
    }
}