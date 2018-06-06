using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using CsvHelper;
using Dapper.Contrib.Extensions;

namespace Exercise.Web
{
    public class StrategyImporter : ICsvImporter
    {
        public StrategyImporter(string strategyFilePath, string connectionString)
        {
            StrategyFilePath = strategyFilePath;
            ConnectionString = connectionString;
        }

        public string StrategyFilePath { get; }
        public string ConnectionString { get; }

        public bool ImportCsv()
        {
            using (var reader = File.OpenText(StrategyFilePath))
            {
                var csv = new CsvReader(reader);
                var strategies = csv.GetRecords<Strategy>().ToList();

                if (!strategies.Any()) return false;

                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    foreach (var strategy in strategies)
                    {
                        if (Enum.TryParse(strategy.Region, out Region region))
                            sqlConnection.Insert(new StrategyDto{Name = strategy.StratName, RegionId = (int) region});
                    }
                }

                return true;
            }

        }
    }
}