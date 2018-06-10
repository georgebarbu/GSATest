using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using Microsoft.Extensions.Logging;

namespace Exercise.Web
{
    public class DatabaseImport : IDatabaseImport
    {
        private readonly ILogger<DatabaseImport> _logger;

        public DatabaseImport(ILogger<DatabaseImport> logger)
        {
            _logger = logger;
        }

        public void CreateDatabase(string masterConnectionString)
        {
            var script = File.ReadAllText("createDB.sql");
            var splitter = new string[] { "\r\nGO\r\n" };
            var commandTexts = script.Split(splitter, StringSplitOptions.RemoveEmptyEntries);

            foreach (var commandText in commandTexts)
            {
                using (var connection = new SqlConnection(masterConnectionString))
                {
                    try
                    {
                        connection.Execute(commandText, commandType: CommandType.Text);
                    }
                    catch (SqlException e)
                    {
                        _logger.LogError(e.ToString());
                        throw;
                    }
                }
            }
        }

        public void InitializeDatabase(string connectionString)
        {
            var script = File.ReadAllText("dbScript.sql");
            var splitter = new string[] { "\r\nGO\r\n" };
            var commandTexts = script.Split(splitter, StringSplitOptions.RemoveEmptyEntries);

            foreach (var commandText in commandTexts)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Execute(commandText, commandType: CommandType.Text);
                    }
                    catch (SqlException e)
                    {
                        _logger.LogError(e.ToString());
                        throw;
                    }
                }
            }
        }
    }

    public interface IDatabaseImport
    {
        void CreateDatabase(string masterConnectionString);
        void InitializeDatabase(string connectionString);
    }
}