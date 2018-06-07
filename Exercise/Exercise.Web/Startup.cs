using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Exercise.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            //var dbImporter = new DatabaseImport();
            //dbImporter.InitializeDatabase();

            //var strategyImporter = new StrategyImporter(@"D:\Work\GSA\properties.csv", connectionString);
            //strategyImporter.ImportCsv();

            //var pnlImporter = new PnLImporter(@"D:\Work\GSA\pnl.csv", connectionString);
            //pnlImporter.ImportCsv();

            //var capitalImport = new CapitalImporter(@"D:\Work\GSA\capital.csv", connectionString);
            //capitalImport.ImportCsv();
        }   
        
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<MyOptions>(myOptions =>
            {
                myOptions.ConnString = Configuration.GetConnectionString("DefaultConnection");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        public class MyOptions
        {
            public string ConnString { get; set; }
        }
    }

}
