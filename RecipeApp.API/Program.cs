using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RecipeApp.Infrastructure.Context;

namespace RecipeApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
			//var host = CreateWebHostBuilder(args).Build();
			//using (var scope = host.Services.CreateScope())
			//{
			//	var services = scope.ServiceProvider;
			//	try
			//	{
			//		var context = services.GetRequiredService<MainContext>();
			//		MigrationManager.Seed(context);
			//	}
			//	catch (Exception ex)
			//	{
			//		var logger = services.GetRequiredService<ILogger<Program>>();
			//		logger.LogError(ex, "An error occurred while seeding the database.");
			//	}
			//}
			//host.Run();
			//CreateHostBuilder(args).Build().Run();

			CreateHostBuilder(args).Build().MigrateDatabase().Run();
		}
		//migraçao auto
		//public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		//{
		//	return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
		//}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
