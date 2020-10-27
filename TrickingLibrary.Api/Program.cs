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
                    ctx.Add(new Difficulty { Id = "easy", Name = "Easy", Description = "Easy tricks" });
                    ctx.Add(new Difficulty { Id = "medium", Name = "Medium", Description = "Medium tricks" });
                    ctx.Add(new Difficulty { Id = "hard", Name = "Hard", Description = "Hard tricks" });

                    ctx.Add(new Category { Id = "dunk", Name = "Dunk", Description = "Dunk types" });
                    ctx.Add(new Category { Id = "layup", Name = "Layup", Description = "Layup types" });
                    ctx.Add(new Category { Id = "shoot", Name = "Shoot", Description = "Shoot types" });

                    ctx.Add(new Trick
                    {
                        Id = "basic-shoot",
                        Name = "Basic Shoot",
                        Description = "Just basic shoot",
                        Difficulty = "easy",
                        TrickCategories = new List<TrickCategory> { new TrickCategory { CategoryId = "shoot" } }
                    });
                    ctx.Add(new Trick
                    {
                        Id = "teardrop",
                        Name = "Teardrop",
                        Description = "Teardrop like SC#30",
                        Difficulty = "medium",
                        TrickCategories = new List<TrickCategory>
                        {
                            new TrickCategory {CategoryId = "layup"},
                            new TrickCategory {CategoryId = "shoot"},
                        },
                        Prerequisites = new List<TrickRelationship> { new TrickRelationship { PrerequisiteId = "basic-shoot" } }
                    });
                    ctx.Add(new Trick
                    {
                        Id = "windmill",
                        Name = "Windmill",
                        Description = "Windmill dunk",
                        Difficulty = "hard",
                        TrickCategories = new List<TrickCategory> { new TrickCategory { CategoryId = "dunk" } }
                    });

                    ctx.Add(new Submission
                    {
                        TrickId = "teardrop",
                        Description = "Teardrop shoots that hard to block",
                        Video = "teardrop.mp4"
                    });
                    ctx.Add(new Submission
                    {
                        TrickId = "windmill",
                        Description = "Destroy hoops with Windmill dunks",
                        Video = "windmill.mp4"
                    });

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
