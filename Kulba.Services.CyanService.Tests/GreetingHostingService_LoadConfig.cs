using Kulba.Services.CyanService.Models;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace Kulba.Services.CyanService.Tests
{
    public class GreetingHostingService_LoadConfig
    {
        private readonly SocketServerConfigInfo _socketServerConfigInfo;

        public GreetingHostingService_LoadConfig(IOptions<SocketServerConfigInfo> socketServerConfigInfo)
        {
            _socketServerConfigInfo = socketServerConfigInfo?.Value ?? throw new ArgumentNullException(nameof(socketServerConfigInfo));
        }

        [Fact]
        public void IsName_ReturnTrue()
        {
            string name = _socketServerConfigInfo.Name;

            Assert.True("Jim".Equals("Jim"));
        }
    }
}
