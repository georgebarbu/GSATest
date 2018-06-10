using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Exercise.Web.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IDatabaseImport _databaseImport;
        private readonly IPnlImporter _pnlImporter;
        private readonly ICapitalImporter _capitalImporter;
        private readonly IStrategyImporter _strategyImporter;

        private string ConnStr { get; }
        private string MasterConnStr { get; }


        public ValuesController(
            ILogger<ValuesController> logger,
            IOptions<Startup.MyConfiguration> optionsAccessor,
            IDatabaseImport databaseImport,
            IPnlImporter pnlImporter,
            ICapitalImporter capitalImporter,
            IStrategyImporter strategyImporter)
        {
            _logger = logger;
            _databaseImport = databaseImport;
            _pnlImporter = pnlImporter;
            _capitalImporter = capitalImporter;
            _strategyImporter = strategyImporter;

            ConnStr = optionsAccessor.Value.ConnString;
            MasterConnStr = optionsAccessor.Value.MasterConnString;
        }

        [HttpGet]
        public string Get()
        {
            _databaseImport.CreateDatabase(MasterConnStr);
            _databaseImport.InitializeDatabase(ConnStr);

            _strategyImporter.ImportStrategy(ConnStr);

            _pnlImporter.ImportPnL(ConnStr);

            _capitalImporter.ImportCapital(ConnStr);

            return "Database initialized";
        }
    }
}
