using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

namespace TrickingLibrary.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

                if (env.IsDevelopment())
                {
                    ctx.Difficulties.Add(new Difficulty { Id = "easy", Name = "Easy", Description = "Easy tricks" });
                    ctx.Difficulties.Add(new Difficulty { Id = "hard", Name = "Hard", Description = "Hard tricks" });

                    ctx.Categories.Add(new Category { Id = "dunk", Name = "Dunk", Description = "Dunk types" });
                    ctx.Categories.Add(new Category { Id = "layup", Name = "Layup", Description = "Layup types" });
                    ctx.Categories.Add(new Category { Id = "shoot", Name = "Shoot", Description = "Shoot types" });

                    ctx.SaveChanges();
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
