using Kulba.Services.CyanService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kulba.Services.CyanService.Services
{
    public class GreetingService : HostedService
    {
        private readonly ILogger<GreetingService> _logger;
        private readonly SocketServerConfigInfo _socketServerConfigInfo;
        private AppSettings appSettings { get; set; }

        public GreetingService(ILogger<GreetingService> logger, IOptions<SocketServerConfigInfo> socketServerConfigInfo)
        {
            _logger = logger;
            _socketServerConfigInfo = socketServerConfigInfo?.Value ?? throw new ArgumentNullException(nameof(socketServerConfigInfo));

            _logger.LogInformation("Starting Greeting Service");

            _logger.LogInformation("SocketServerConfigInfo: " + _socketServerConfigInfo.ToString());

        }

        //protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        //{
        //    CancellationTokenSource source = new CancellationTokenSource();
        //    CancellationToken token = source.Token;
        //    while (!cancellationToken.IsCancellationRequested)
        //    {
        //        for (int i = 1; i < 5; i++)
        //        {
        //            _logger.LogInformation("Greeting Service Says Hello Joe at: {time}", DateTime.Now);
        //            _logger.LogDebug("Iteration: " + i);
        //            if (i == 4)
        //            {
        //                source.Cancel();
        //                Console.WriteLine("Cancelling at task {0}", i);
        //                cancellationToken = token;
        //                break;
        //            }
        //        }
        //        await Task.Delay(1000, cancellationToken);
        //    }
        //}


        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Greeting Service Says Hello Joe at: {time}", DateTime.Now);
                await Task.Delay(1000, cancellationToken);    
            }
        }

    }
}
