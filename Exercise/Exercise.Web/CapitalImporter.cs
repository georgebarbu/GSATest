using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using CsvHelper;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;

namespace Exercise.Web
{
    public class CapitalImporter : ICapitalImporter
    {
        private readonly ILogger<CapitalImporter> _logger;

        public CapitalImporter(ILogger<CapitalImporter> logger)
        {
            _logger = logger;
        }

        public bool ImportCapital(string connectionString)
        {
            using (var reader = File.OpenText("Capital.csv"))
            {
                var csv = new CsvReader(reader);
                var capitalList = csv.GetRecords<Capital>().ToList();
                if (!capitalList.Any()) return false;
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    var strategies = sqlConnection.Query<StrategyDto>("GetStrategies")
                        .ToDictionary(k => k.Name, k => k.Id);

                    foreach (var capital in capitalList)
                    {
                        var strategiesProperties = typeof(Capital).GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.Name.Contains("Strategy"));
                        foreach (var propertyInfo in strategiesProperties)
                        {
                            sqlConnection.Insert(new CapitalDto
                            {
                                Capital = (decimal)propertyInfo.GetValue(capital, null),
                                Date = capital.Date,
                                StrategyId = strategies[propertyInfo.Name]
                            });
                        }
                    }
                }

                return true;

            }
        }
        public sealed class Capital
        {
            public DateTime Date { get; set; }
            public decimal Strategy1 { get; set; }
            public decimal Strategy2 { get; set; }
            public decimal Strategy3 { get; set; }
            public decimal Strategy4 { get; set; }
            public decimal Strategy5 { get; set; }
            public decimal Strategy6 { get; set; }
            public decimal Strategy7 { get; set; }
            public decimal Strategy8 { get; set; }
            public decimal Strategy9 { get; set; }
            public decimal Strategy10 { get; set; }
            public decimal Strategy11 { get; set; }
            public decimal Strategy12 { get; set; }
            public decimal Strategy13 { get; set; }
            public decimal Strategy14 { get; set; }
            public decimal Strategy15 { get; set; }
        }
    }

    [Table("Capital")]
    public sealed class CapitalDto
    {
        [Computed]
        public int Id { get; set; }

        public decimal Capital { get; set; }

        public int StrategyId { get; set; }

        public DateTime Date { get; set; }
    }
}