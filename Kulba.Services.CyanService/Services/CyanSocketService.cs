using Kulba.Services.CyanService.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;


namespace Kulba.Services.CyanService.Services
{
    public class CyanSocketService : HostedService
    {
        private readonly ILogger<CyanSocketService> _logger;
        private readonly SocketServerConfigInfo _socketServerConfigInfo;

        public CyanSocketService(ILogger<CyanSocketService> logger, IOptions<SocketServerConfigInfo> socketServerConfigInfo)
        {
            _logger = logger;
            _socketServerConfigInfo = socketServerConfigInfo?.Value ?? throw new ArgumentNullException(nameof(socketServerConfigInfo));

            _logger.LogInformation("Starting CyanSocketService with the following parameters: " + _socketServerConfigInfo.ToString());

        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {

                TcpListener listener = new TcpListener(System.Net.IPAddress.Any, _socketServerConfigInfo.Port);
                listener.Start();

                while (true)
                {
                    _logger.LogInformation("Waiting for a connection...");
                    TcpClient client = listener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    StreamReader streamReader = new StreamReader(client.GetStream());
                    StreamWriter streamWriter = new StreamWriter(client.GetStream());

                    try
                    {
                        byte[] buffer = new byte[1024];
                        stream.Read(buffer, 0, buffer.Length);
                        int recv = 0;
                        foreach (byte b in buffer)
                        {
                            if (b != 0)
                            {
                                recv++;
                            }
                        }
                        string request = Encoding.UTF8.GetString(buffer, 0, recv);
                        _logger.LogInformation("Request received: " + request);
                        streamWriter.WriteLine("SUCCESS");
                        streamWriter.Flush();

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Exception during Receivig message.", ex);
                        throw;
                    }
                    await Task.Delay(100, cancellationToken);
                }
            }
        }
    }
}
