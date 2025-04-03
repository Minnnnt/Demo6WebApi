using Hangfire;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Application.Hangfires
{
    public class TeachingWorkloadStatisticsHangfireJobService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // 一次性任务
            BackgroundJob.Enqueue(() => Console.WriteLine("一次性 Hangfire 任务正在运行！"));

            BackgroundJob.Schedule(() => Console.WriteLine("Scheduled Hangfire job is running!"), TimeSpan.FromMinutes(1));

            // 定期任务
            RecurringJob.AddOrUpdate("MyRecurringJob", () => Console.WriteLine("定期 Hangfire 任务正在运行！"), Cron.Minutely);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
