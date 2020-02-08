using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Routine.Api.Data;
using Routine.Api.Services;
using AutoMapper;
namespace Routine.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //注册服务，已经注册的服务可以在项目其它地方通过依赖注入使用
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers(); //源码
           
            // option.ReturnHttpNotAcceptable = true  请求格式和接受模式必须一致。否组返回406
            services.AddControllers(option =>
            {
                option.ReturnHttpNotAcceptable = true;
                ////默认返回格式是json。此处添加返回类型 xml
                //option.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            }).AddXmlDataContractSerializerFormatters();

            //注册 对象映射器 
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddScoped<ICompanyRepository, CompanyRepository>();//每次http请求都执行一次


            services.AddDbContext<RoutineDbContext>(option => 
            {
                option.UseSqlite("Data Source=routine.db");
            });
        }

        //指定asp.net core web程序 是如何响应每一个http请求=》在这里配置请求的管道【在此处添加中间件】
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();//路由中间件

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
