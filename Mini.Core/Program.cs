using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mini.CoreApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                .UseStartup<Startup>()
                .UseUrls("http://*:8081")
                .ConfigureLogging((hostingContext, builder) =>
                {
                    // 1.���˵�ϵͳĬ�ϵ�һЩ��־
                    builder.AddFilter("System", LogLevel.Error);
                    builder.AddFilter("Microsoft", LogLevel.Error);

                    // 2.Ҳ������appsettings.json�����ã�LogLevel�ڵ�

                    // 3.ͳһ����
                    builder.SetMinimumLevel(LogLevel.Error);

                    // Ĭ��log4net.confg
                    builder.AddLog4Net(Path.Combine(Directory.GetCurrentDirectory(), "Log4net.config"));
                });
            });
        }
    }
}
