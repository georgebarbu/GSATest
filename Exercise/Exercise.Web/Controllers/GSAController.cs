using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exercise.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/GSA")]
    public class GSAController : Controller
    {
        // GET: api/GSA
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GSA/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/GSA
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
