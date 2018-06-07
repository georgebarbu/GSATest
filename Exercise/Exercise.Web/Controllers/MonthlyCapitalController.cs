using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exercise.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Monthly-Capital")]
    public class MonthlyCapitalController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{strategies}")]
        public string Get(string strategies)
        {

            return strategies;
        }
    }
}