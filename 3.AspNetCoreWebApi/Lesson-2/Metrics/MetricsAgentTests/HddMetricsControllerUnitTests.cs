using System;
using Xunit;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class HddMetricsControllerUnitTests
    {

        HddMetricsController _controller;

        public HddMetricsControllerUnitTests()
        {
            _controller = new HddMetricsController();
        }

        [Fact]
        public void GetMetrics_ReturnOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            
            var result = _controller.GetMetrics(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
