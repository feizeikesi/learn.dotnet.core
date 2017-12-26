using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Route
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


            //注册一个路由约束
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("evenint", typeof(EvenIntRouteConstraint));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
            //状态码页面中间件必须在 UseMvc 之前
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "custom",
                    template: "Calculator/Calculate({a:evenint},{b:evenint})");
            });

            //默认未匹配到的页面路由 
            app.UseMvc(routes =>
            {
                //default routes go here

                routes.MapRoute(
                    name: "CatchAll",
                    template: "{*url}",
                    defaults: new {controller = "CatchAll", action = "CatchAll"}
                );
            });

            //配合app.UseStatusCodePagesWithReExecute 使用
            app.UseMvc(routes =>
            {
                //default routes go here
                routes.MapRoute(
                    name: "Error404",
                    template: "error/404",
                    defaults: new { controller = "CatchAll", action = "Error404" }
                );

            });

           
            //等同上边
            app.UseMvcWithDefaultRoute();
        }
    }
}