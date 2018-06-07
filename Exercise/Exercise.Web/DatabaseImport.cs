using System;
using Dapper;
using CsvHelper;

namespace Exercise.Web
{
    public class DatabaseImport : IDatabaseImport
    {
        public void InitializeDatabase()
        {
            // Create database, tables, and so on here
        }
    }

    public interface IDatabaseImport
    {
        void InitializeDatabase();
    }
}