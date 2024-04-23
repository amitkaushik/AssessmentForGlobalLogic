using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using WindowService_POC.Tasks;
using WindowService_POC.Contract;
using System.Threading.Tasks;
using WindowService_POC.Implementation;
using WindowService_POC.DataContext;

namespace WindowService_POC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IHost Host = CreateHostBuilder(args).Build();

            await Host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .UseWindowsService()
               .ConfigureServices(services =>
               {
                   ConfigureQuartzService(services);

                   services.AddScoped<ITaskLogTime, TaskLogTime>();
                   services.AddScoped<SDBContext>();
               });

        private static void ConfigureQuartzService(IServiceCollection services)
        {
            // Add the required Quartz.NET services
            services.AddQuartz(q =>
            {
                // Use a Scoped container to create jobs.
                q.UseMicrosoftDependencyInjectionJobFactory();

                // Create a "key" for the job
                var jobKey = new JobKey("Task1");

                // Register the job with the DI container
                q.AddJob<Task1>(opts => opts.WithIdentity(jobKey));

                // Create a trigger for the job
                q.AddTrigger(opts => opts
                    .ForJob(jobKey) // link to the Task1
                    .WithIdentity("Task1-trigger") // give the trigger a unique name
                    .WithCronSchedule("20/5 * * * * ?")); //Setup time 
            });

            // Add the Quartz.NET hosted service
            services.AddQuartzHostedService(
                q => q.WaitForJobsToComplete = true);
        }
    }
}
