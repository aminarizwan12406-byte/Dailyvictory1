using System;
using System.Data.SQLite;
using System.IO;

namespace Dailyvictory
{
    class DatabaseHelper
    {
        string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Dailyvictory.db");

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection($"Data Source={dbPath};Version=3;");
        }
    }
}