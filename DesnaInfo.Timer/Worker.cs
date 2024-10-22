using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesnaInfo.Timer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private TimeSpan tsp;
        private HttpClient client;

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();
            //client.DefaultRequestHeaders.Add("X-Viber-Auth-Token", "4d3354becb67deb0-be58a8967c84442e-2a21103968d0ab94");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            client?.Dispose();
            return base.StopAsync(cancellationToken);
        }

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var now = DateTime.Now;
                var sendDate = new DateTime(now.Year, now.Month, now.Day, 8, 00, 00);
                if (sendDate <= now)
                {
                    sendDate = sendDate.AddDays(1);
                }
                tsp = sendDate - now;

                await Task.Delay(tsp, stoppingToken);

                await client.PostAsync("https://444198-vds-homi4work.gmhost.pp.ua/api/info/Broadcast",
                    new StringContent("\"content\"", Encoding.UTF8, "application/json"), stoppingToken);
                
            }
        }
    }
}
