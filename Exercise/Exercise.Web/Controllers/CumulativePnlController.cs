using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Exercise.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Cumulative-Pnl")]
    public class CumulativePnlController : Controller
    {
        public string ConnStr { get; set; }

        public CumulativePnlController(IOptions<Startup.MyOptions> optionsAccessor)
        {
            ConnStr = optionsAccessor.Value.ConnString;
        }

        [HttpGet]
        public IEnumerable<CumulativePnlResult> Get(string startDate, string region)
        {
            var regions = region.Split(',');

            using (var sqlConn = new SqlConnection(ConnStr))
            {
                const string sql = @"SELECT DISTINCT s.Name as Strategy, p.Date, p.PnL, r.Name as Region 
                                    FROM dbo.Strategy s
                                    INNER JOIN dbo.Region r ON r.Id = s.RegionId
                                    INNER JOIN dbo.PnL p ON p.StrategyId = s.Id
                                    WHERE p.Date >= @StartDate AND r.Name IN @Regions 
                                    GROUP BY s.Name, p.Date, p.PnL, r.Name
                                    ORDER BY p.Date";

                var results = sqlConn.Query<PnlResult>(sql, 
                    new { StartDate = startDate, Regions = regions }, 
                    commandType:CommandType.Text).ToList();

                var cummulativePnlResult = new List<CumulativePnlResult>();
                foreach (var r in regions)
                {
                    var regionPnLs = results.GroupBy(e => e.Date)
                        .Select(pnlGroup => new {Date = pnlGroup.Key, Pnl = pnlGroup.Sum(p => p.PnL)});

                    decimal accumulator = 0;
                    foreach (var regionPnL in regionPnLs)
                    {
                        accumulator += regionPnL.Pnl;
                        cummulativePnlResult.Add(new CumulativePnlResult
                        {
                            CumulativePnL = accumulator,
                            Date = regionPnL.Date,
                            Region = r
                        });
                    }
                }

                return cummulativePnlResult;
            }
        }
    }

    public sealed class PnlResult
    {
        public string Strategy { get; set; }
        public string Region { get; set; }
        public DateTime Date { get; set; }
        public decimal PnL { get; set; }
    }

    public sealed class CumulativePnlResult
    {
        public string Region { get; set; }
        public DateTime Date { get; set; }
        public decimal CumulativePnL { get; set; }
    }
        
}