using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using CsvHelper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;

namespace Exercise.Web
{
    public class StrategyImporter : IStrategyImporter
    {
        private readonly ILogger<StrategyImporter> _logger;

        public StrategyImporter(ILogger<StrategyImporter> logger)
        {
            _logger = logger;
        }

        public bool ImportStrategy(string connectionString)
        {
            using (var reader = File.OpenText("properties.csv"))
            {
                var csv = new CsvReader(reader);
                var strategies = csv.GetRecords<Strategy>().ToList();

                if (!strategies.Any()) return false;

                using (var sqlConnection = new SqlConnection(connectionString))
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