using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Configuration
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            //添加一个 JsonConfigurationSource   
            var jsonSource = new JsonConfigurationSource {Path = "filename.json"};
            builder.Add(jsonSource);

            builder
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .AddXmlFile("web.config");

            //添加特定环境
            builder
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json");

            //添加一个 文件
            builder.AddJsonFile("filename.json");



            var cfg = builder.Build();


            var token = cfg.GetReloadToken();
            token.RegisterChangeCallback(callback: (state) =>
            {
                //state will be someData
                //push the changes to whoever needs it
            }, state: cfg);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}