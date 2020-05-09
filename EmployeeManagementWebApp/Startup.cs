using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementWebApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagementWebApp
{
    public class Startup
    {
        private IConfiguration _config;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration config)
        {
            _config = config;
            
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMvcCore();
            services.AddSingleton<IEmployeeReposiroty,MockEmployeeRepositroy>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //DeveloperExceptionPageOptions exceptionPageOptions = new DeveloperExceptionPageOptions
                //{
                //    SourceCodeLineCount = 1
                //};
                //app.UseDeveloperExceptionPage(exceptionPageOptions);
            }

            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("Home.html");
            //app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("Home.html");
            //  app.UseFileServer();
            app.Run(async (context) =>
            {
                //throw new Exception("This is an exception method");
                //await context.Response.WriteAsync("Hello World!");
                await context.Response.WriteAsync("MVC Route:");
            });
        }
    }
}
