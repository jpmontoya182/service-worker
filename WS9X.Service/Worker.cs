using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WS9X.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IConfiguration Configuration { get; }


        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            // get appsettings.json
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Int32 executionLoopTime = Convert.ToInt32(Configuration.GetSection("settings:ExecutionLoopTime").Value);
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(executionLoopTime, stoppingToken);
            }
        }
    }
}
