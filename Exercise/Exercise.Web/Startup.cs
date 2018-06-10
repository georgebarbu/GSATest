using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Exercise.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

                    }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<MyConfiguration>(myOptions =>
            {
                myOptions.ConnString = Configuration.GetConnectionString("DefaultConnection");
                myOptions.MasterConnString = Configuration.GetConnectionString("MasterDBConnection");
            });

            services.Add(new ServiceDescriptor(typeof(IDatabaseImport), typeof(DatabaseImport), ServiceLifetime.Singleton));
            services.Add(new ServiceDescriptor(typeof(IPnlImporter), typeof(PnLImporter), ServiceLifetime.Singleton));
            services.Add(new ServiceDescriptor(typeof(IStrategyImporter), typeof(StrategyImporter), ServiceLifetime.Singleton));
            services.Add(new ServiceDescriptor(typeof(ICapitalImporter), typeof(CapitalImporter), ServiceLifetime.Singleton));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddLog4Net();
           
            app.UseMvc();
        }

        public class MyConfiguration
        {
            public string ConnString { get; set; }
            public string MasterConnString { get; set; }
        }
    }

}
