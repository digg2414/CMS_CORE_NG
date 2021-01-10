using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core.Enrichers;

namespace LoggingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            var scope = 
            
            host.Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSerilog((hostingContext, LoggerConfiguration) => LoggerConfiguration
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("Application", "CMS APP")
                        .Enrich.WithProperty("MachineName", Environment.MachineName)
                        .Enrich.WithProperty("CurrentManageThreadId", Environment.CurrentManagedThreadId)
                        .Enrich.WithProperty("OSVersion", Environment.OSVersion)
                        .Enrich.WithProperty("Version", Environment.Version)
                        .Enrich.WithProperty("Username", Environment.UserName)
                        .Enrich.WithProperty("ProcessId", Process.GetCurrentProcess().Id)
                        .Enrich.WithProperty("ProcessName", Process.GetCurrentProcess().ProcessName)
                        .WriteTo.File(formatter: new CustomTextFormatter(),
                            path: Path.Combine(
                                hostingContext.HostingEnvironment.ContentRootPath +
                                $"{Path.DirectorySeparatorChar}Logs{Path.DirectorySeparatorChar}",
                                $"cms_core_ng_{DateTime.Now:yyyyMMdd}.txt"))
                        .ReadFrom.Configuration(hostingContext.Configuration));
                    webBuilder.UseStartup<Startup>(); 
                    
                });
    }
}