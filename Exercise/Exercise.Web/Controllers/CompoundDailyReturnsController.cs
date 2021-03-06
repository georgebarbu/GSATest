﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Exercise.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Compound-Daily-Returns")]
    public class CompoundDailyReturnsController : Controller
    {
        public string ConnStr { get; set; }

        public CompoundDailyReturnsController(IOptions<Startup.MyConfiguration> optionsAccessor)
        {
            ConnStr = optionsAccessor.Value.ConnString;
        }

        [HttpGet("{strategy}")]
        public IEnumerable<CompoundDailyReturnsResult> Get(string strategy)
        {




            return new List<CompoundDailyReturnsResult>();
        }
    }

    public class CompoundDailyReturnsResult
    {
    }
}