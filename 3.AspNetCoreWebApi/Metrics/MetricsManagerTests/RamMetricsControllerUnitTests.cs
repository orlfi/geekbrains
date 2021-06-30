using System;
using Xunit;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerTests
{
    public class RamMetricsControllerUnitTests
    {
        private readonly RamMetricsController _controller;
        public RamMetricsControllerUnitTests()
        {
            _controller = new RamMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOK()
        {
            int agentId = 1;
            var fromTime = DateTimeOffset.Now.AddDays(-5);
            var toTime =  DateTimeOffset.Now;

            var result = _controller.GetMetricsFromAgent(agentId, fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromCluster_ReturnsOK()
        {
            var fromTime = DateTimeOffset.Now.AddDays(-5);
            var toTime =  DateTimeOffset.Now;

            var result = _controller.GetMetricsFromAllCluster(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
