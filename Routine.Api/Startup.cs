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

        //ע������Ѿ�ע��ķ����������Ŀ�����ط�ͨ������ע��ʹ��
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers(); //Դ��
           
            // option.ReturnHttpNotAcceptable = true  �����ʽ�ͽ���ģʽ����һ�¡����鷵��406
            services.AddControllers(option =>
            {
                option.ReturnHttpNotAcceptable = true;
                ////Ĭ�Ϸ��ظ�ʽ��json���˴���ӷ������� xml
                //option.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            }).AddXmlDataContractSerializerFormatters();

            //ע�� ����ӳ���� 
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddScoped<ICompanyRepository, CompanyRepository>();//ÿ��http����ִ��һ��


            services.AddDbContext<RoutineDbContext>(option => 
            {
                option.UseSqlite("Data Source=routine.db");
            });
        }

        //ָ��asp.net core web���� �������Ӧÿһ��http����=����������������Ĺܵ����ڴ˴�����м����
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();//·���м��

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
