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


        public void ImportCapital()
        {
            throw new NotImplementedException();
        }

        public void ImportPnL()
        {
            throw new NotImplementedException();
        }
    }

    public interface IDatabaseImport
    {
        void InitializeDatabase();
        //void ImportStrategies();
        //void ImportCapital();
        //void ImportPnL();
    }
}