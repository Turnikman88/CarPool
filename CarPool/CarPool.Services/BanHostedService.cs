using CarPool.Services.Data.Contracts;
using CarPool.Services.Data.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarPool.Services
{
    public class BanHostedService : IHostedService, IDisposable
    {
        private Timer _timer = null!;
        private readonly IApplicationUserService _us;

        public BanHostedService()
        {
           // this._us = us;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            // await _us.RemoveBanAsync();
            await Task.Run(() => Console.WriteLine("work"));
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}