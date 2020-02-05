using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Routine.Api.Data;

namespace Routine.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();

            var host = CreateHostBuilder(args).Build();

            using (var scope =host.Services.CreateScope())
            {
                try
                {
                    var dbContext = scope.ServiceProvider.GetService<RoutineDbContext>();


                    //ÿ������ɾ�����ݿ�
                    dbContext.Database.EnsureDeleted();
                    //ɾ�����ݿ�֮��Ǩ��һ��
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {

                    //ͨ����־��¼�쳣
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Database Migration Error");
                  
                }
            }
                host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
