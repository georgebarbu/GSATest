using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using CsvHelper;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Exercise.Web
{
    public class PnLImporter : ICsvImporter
    {
        public string PnlFilePath { get; }
        public string ConnectionString { get; }

        public PnLImporter(string pnlFilePath, string connectionString)
        {
            PnlFilePath = pnlFilePath;
            ConnectionString = connectionString;
        }

        public bool ImportCsv()
        {
            using (var reader = File.OpenText(PnlFilePath))
            {
                var csv = new CsvReader(reader);
                var pnls = csv.GetRecords<Pnl>().ToList();
                if (!pnls.Any()) return false;
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    var strategies = sqlConnection.Query<StrategyDto>("GetStrategies")
                        .ToDictionary(k => k.Name, k => k.Id);

                    foreach (var pnl in pnls)
                    {
                        var strategiesProperties = typeof(Pnl).GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.Name.Contains("Strategy"));
                        foreach (var propertyInfo in strategiesProperties)
                        {
                            sqlConnection.Insert(new PnlDto
                            {
                                PnL = (decimal) propertyInfo.GetValue(pnl, null),
                                Date = pnl.Date,
                                StrategyId = strategies[propertyInfo.Name]
                            });
                        }
                    }
                }

                return true;

            }
        }
    }

    public sealed class Pnl
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

    [Table("PnL")]
    public sealed class PnlDto
    {
        [Computed]
        public int Id { get; set; }

        public decimal PnL { get; set; }

        public int StrategyId { get; set; }

        public DateTime Date { get; set; }
    }
}