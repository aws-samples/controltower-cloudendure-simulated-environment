using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DMSSample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DMSSample.Services;
using Microsoft.Extensions.Logging;
using Amazon.DatabaseMigrationService;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.AspNetCore.Diagnostics;

namespace DMSSample
{
    public class Startup
    {
        // public Startup(IConfiguration configuration)
        // {
        //     Configuration = configuration;
        // }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.Configure<CookiePolicyOptions>(options => {
            //     options.CheckConsentNeeded = ContextBoundObject => true;
            //     options.MinimumSameSitePolicy = SameSiteMode.None;
            // });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddEntityFrameworkSqlServer().AddDbContext<SQLContext>();
            services.AddEntityFrameworkSqlServer().AddDbContext<MySQLContext>();

            services.AddScoped<IDbContextService, DbContextService>();
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddOptions();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonDatabaseMigrationService>(ServiceLifetime.Singleton);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment() && false )
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context => 
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/html";

                        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                        await context.Response.WriteAsync("<h1>ERROR!</h1><br><br>\r\n");

                        var exceptionHandlerPathFeature = 
                            context.Features.Get<IExceptionHandlerPathFeature>();

                        // Use exceptionHandlerPathFeature to process the exception (for example, 
                        // logging), but do NOT expose sensitive error information directly to 
                        // the client.

                        //if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                        //{
                            await context.Response.WriteAsync("<pre>\r\n");
                            await context.Response.WriteAsync(exceptionHandlerPathFeature?.Error.Message);
                            await context.Response.WriteAsync("</pre>\r\n");
                            await context.Response.WriteAsync("<h1>STACK</h1><br><br>\r\n");
                            await context.Response.WriteAsync("<pre>");
                            await context.Response.WriteAsync(exceptionHandlerPathFeature?.Error.StackTrace);
                            await context.Response.WriteAsync("</pre>");
                        //}

                        await context.Response.WriteAsync("<br><a href=\"/\">Home</a><br>\r\n");
                        await context.Response.WriteAsync("</body></html>\r\n");
                        await context.Response.WriteAsync(new string(' ', 512)); // IE padding
                    });
                });
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            //app.UseCookiePolicy();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
