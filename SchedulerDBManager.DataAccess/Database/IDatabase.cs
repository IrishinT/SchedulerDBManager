using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace SchedulerDBManager.DataAccess.Database
{
    public interface IDatabase
    {
        DataTable ExecuteSelect(string query, params OleDbParameter[] parameters);
        void ExecuteNonQuery(string query, params OleDbParameter[] parameters);
    }
}