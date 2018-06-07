using System;
using System.Collections.Generic;
using System.Data;
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
    [Route("api/Monthly-Capital")]
    public class MonthlyCapitalController : Controller
    {
        public string ConnStr { get; set; }

        public MonthlyCapitalController(IOptions<Startup.MyOptions> optionsAccessor)
        {
            ConnStr = optionsAccessor.Value.ConnString;
        }

        [HttpGet]
        public IEnumerable<CapitalResult> Get(string strategies)
        {
            if (string.IsNullOrWhiteSpace(strategies))
            {
                using (var sqlConn = new SqlConnection(ConnStr))
                {
                    var results = sqlConn.Query<CapitalResult>("GetAllMonthlyCapital", commandType: CommandType.StoredProcedure);
                    return results;
                }
            }

            var strategiesList = strategies.Split(',');
            using (var sqlConn = new SqlConnection(ConnStr))
            {
                string sql = @"SELECT s.Name AS Strategy,
		                                   c.Date,
		                                   c.Capital
	                                FROM dbo.Capital c
		                                INNER JOIN dbo.Strategy s
			                                ON c.StrategyId = s.Id
	                                WHERE s.Name IN @Strategies
                                    GROUP BY s.Name, c.Date, c.Capital";

                var results = sqlConn.Query<CapitalResult>(sql,
                    new {Strategies = strategiesList},
                    commandType: CommandType.Text);

                return results;
            }
        }
    }

    [Serializable]
    public sealed class CapitalResult
    {
        public string Strategy { get; set; }
        public DateTime Date { get; set; }
        public decimal Capital { get; set; }
    }

}