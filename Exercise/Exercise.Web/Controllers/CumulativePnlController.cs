using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return null;
        }
    }

    public sealed class CumulativePnlResult
    {

    }
}