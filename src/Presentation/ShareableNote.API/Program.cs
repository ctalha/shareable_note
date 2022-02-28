using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer;
using System;

namespace ShareableNote.API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var logDB = @"Server=localhost;Initial Catalog=SHARED_NOTE;User ID=sa;Password=sql123;";
            var sinkOpts = new MSSqlServerSinkOptions();
            sinkOpts.TableName = "Log";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(new CompactJsonFormatter(), "./Logs/Log.json", rollingInterval: RollingInterval.Day)
                .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                .WriteTo.MSSqlServer(
                        connectionString: logDB,
                        sinkOptions: sinkOpts
                 )
                .CreateLogger();

            try
            {
                Log.Information("Application starting up.");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application failed to start up correctly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
