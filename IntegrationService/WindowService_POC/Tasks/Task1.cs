using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WindowService_POC.Contract;

namespace WindowService_POC.Tasks
{
    class Task1 : IJob
    {
        private readonly IServiceProvider _serviceProvider;

        public Task1(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using var scope = _serviceProvider.CreateScope();
            var svc = scope.ServiceProvider.GetRequiredService<ITaskLogTime>();
            await svc.DoWork(context.CancellationToken);

            await Task.CompletedTask;
        }
    }
}
