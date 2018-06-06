using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Ruanmou.NetCore.MVC6.Unitility.Filters;
using Ruanmou.NetCore.MVC6.Unitility.Middleware;
using Ruanmou.NetCore.Servcie;
using Ruanmou.NetCore.Interface;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Ruanmou.EFCore.Model;
using Microsoft.EntityFrameworkCore;

namespace Ruanmou.NetCore.MVC6
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    // Add framework services.
        //    services.AddMvc(o =>
        //    {
        //        o.Filters.Add(typeof(CustomExceptionFilterAttribute));//全局注册filter
        //    });

        //    services.AddSession(o =>
        //    {
        //        o.IdleTimeout = TimeSpan.FromSeconds(60);
        //    });

        //    services.AddTransient<ITestServiceA, TestServiceA>();//瞬时
        //    services.AddSingleton<ITestServiceC, TestServiceC>();//单例
        //    services.AddScoped<ITestServiceB, TestServiceB>();//应用程序,单个请求一个
        //}

            /// <summary>
            /// 容器换自定义的，需要改方法返回值
            /// </summary>
            /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(o =>
            {
                o.Filters.Add(typeof(CustomExceptionFilterAttribute));//全局注册filter
            });

            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromSeconds(60);
            });
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<TestServiceA>().As<ITestServiceA>();
            containerBuilder.RegisterType<TestServiceC>().As<ITestServiceC>();
            containerBuilder.RegisterType<TestServiceB>().As<ITestServiceB>();

            containerBuilder.RegisterType<JDContext>().As<DbContext>().InstancePerDependency();
            containerBuilder.RegisterType<BaseService>().As<IBaseService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<CategoryService>().As<ICategoryService>();
            containerBuilder.RegisterType<CommodityService>().As<ICommodityService>();
            containerBuilder.RegisterType<UserMenuService>().As<IUserMenuService>();

            containerBuilder.Populate(services);

            IContainer container = containerBuilder.Build();
            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //RequestDelegate
            ////注册middleware  Run 注册终结点
            //app.Run(async (HttpContext context) =>
            //{
            //    await context.Response.WriteAsync("Hello World Run");
            //});

            //注册middleware  Use   不执行next()  就等于是run()
            //执行，就可以执行下一个中间件
            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("123456");
            //    //await context.Response.WriteAsync("Hello World Use <br/>");
            //    await next();
            //});

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("123456");
            //    //await context.Response.WriteAsync("Hello World Use Two <br/>");
            //    await next();
            //});

            ////user类
            //app.UseMiddleware<ElevenStopMiddleware>();

            //app.UseMiddleware<ElevenMiddleware>();

            //app.UseMiddleware<ElevenSecondMiddleware>();

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello World Use Two <br/>");
            //    //await next();
            //});

            app.Map("/test", MapTest);
            app.MapWhen(context =>
            {
                return context.Request.Query.ContainsKey("a");
                //拒绝非chorme浏览器的请求
            }, MapTest);

            app.UseSession();
            app.UseStaticFiles();
            app.UseMvc(routes =>//mvc没有next()
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //middleware和filter的区别？
            //middleware管道模型，，只能粗略过滤下请求
            //filter是mvc这个中间件里面的，是mvc的一部分，可以更精准的控制请求
        }

        private static void MapTest(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"Url is {context.Request.Path.Value}");
            });
        }
    }
}
