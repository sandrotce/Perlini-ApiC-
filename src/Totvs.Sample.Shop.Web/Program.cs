using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Totvs.Sample.Shop.Web {
    public class Program {
        public static void Main (string[] args) {
            Console.Title = "TOTVS Shop API";

            var host = WebHost.CreateDefaultBuilder (args)
                .ConfigureAppConfiguration ((hostingContext, config) => {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile ($"appsettings.{env.EnvironmentName}.json", optional : false, reloadOnChange : true);

                    config.AddEnvironmentVariables ();
                    config.AddCommandLine (args);
                })
                .ConfigureLogging ((hostingContext, logging) => {
                    Log.Logger = new LoggerConfiguration ()
                        .ReadFrom.Configuration (hostingContext.Configuration)
                        .CreateLogger ();

                })
                .UseStartup<Startup> ()
                .UseSerilog ()
                .UseKestrel ()
                .UseContentRoot (Directory.GetCurrentDirectory ())
                .UseIISIntegration ()
                .UseSetting ("detailedErrors", "true")
                .Build ();

            host.Run ();

            Log.CloseAndFlush ();
        }
    }
}