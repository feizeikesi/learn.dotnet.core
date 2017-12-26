using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControllersAndActions
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

            //poco 方式需要注入默认的IHttpContextAccessor和IUrlHelperFactory 的实现
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUrlHelperFactory, UrlHelperFactory>();

            //注册的顺序很重要

            //从提交的表单中注入值，例如， <input type="text" name="myParam"/>
            services.AddMvc(options => { options.ValueProviderFactories.Add(new FormValueProviderFactory()); });
            //路由参数，例如， [controller]/[action]/{id?}
            services.AddMvc(options => { options.ValueProviderFactories.Add(new RouteValueProviderFactory()); });
            //查询字符串值，例如， ?id=100
            services.AddMvc(options => { options.ValueProviderFactories.Add(new QueryStringValueProviderFactory()); });
            //jQuery表单值
            services.AddMvc(options => { options.ValueProviderFactories.Add(new JQueryFormValueProviderFactory()); });

            //自定义 cookie
            services.AddMvc(options => { options.ValueProviderFactories.Add(new CookieValueProviderFactory()); });

            /**
             * 模型绑定
             * public IActionResult Process([HtmlEncode] string html) { ... }
             */
            services.AddMvc(options => { options.ModelBinderProviders.Add(new HtmlEncodeModelBinderProvider()); });

            //模型验证
            services.AddMvc(options => { options.ModelValidatorProviders.Add(new CustomModelValidatorProvider()); });

            //获取api 版本
            services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new HeaderApiVersionReader(
                    "api-version");
            });
            /*
             * 从URL 获取api 版本
             * /api/v2/
             */
            services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
            });

            //默认api 版本

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(2, 0);
                options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
            });

            //缓存
            services.AddMemoryCache();

            //临时数据
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddSingleton<ITempDataProvider, SessionStateTempDataProvider>();
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}