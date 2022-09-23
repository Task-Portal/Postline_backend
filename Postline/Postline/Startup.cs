using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Contracts;
using LoggerService;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using Postline.Extensions;
using Postline.Presentation.ActionFilters;

namespace Postline
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
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.AddControllers();
            services.ConfigureRepositoryManager();
            services.ConfigureServiceManager();
            services.ConfigureSqlContext(Configuration);
            services.ConfigureEmailService(Configuration);
            services.AddControllers()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
            services.AddAutoMapper(typeof(Program));
            services.AddScoped<ValidationFilterAttribute>();
            
            // throtting 
            services.AddMemoryCache();
            services.ConfigureRateLimitingOptions();
            services.AddHttpContextAccessor();



            services.AddAuthentication(); 
            services.ConfigureIdentity(); 
            
            services.ConfigureJWT(Configuration); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var logger = app.ApplicationServices.GetRequiredService<ILoggerManager>();
            app.ConfigureExceptionHandler(logger);
            if (env.IsProduction())
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();  
            app.UseIpRateLimiting();

            app.UseRouting();
            
            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseCors("CorsPolicy");
            
            app.UseAuthentication(); 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
