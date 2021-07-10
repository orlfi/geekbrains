using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using MediatR;
using MetricsAgent.Features.Queries;
using MetricsAgent.Responses;

namespace MetricsAgentTests
{
    public class HddMetricsControllerUnitTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<HddMetricsController>> _mockLogger;
        private readonly HddMetricsController _controller;

        public HddMetricsControllerUnitTests()
        {
            _mockLogger = new Mock<ILogger<HddMetricsController>>();
            _mockMediator = new Mock<IMediator>();
            _controller = new HddMetricsController(_mockLogger.Object, _mockMediator.Object);
        }

        [Fact]
        public async Task GetMetricsByPeriod_ReturnOk()
        {
            var request = new HddMetricGetByPeriodQuery()
            {
                FromTime = DateTimeOffset.Now.AddDays(-5),
                ToTime = DateTimeOffset.Now
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<HddMetricGetByPeriodQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HddMetricResponse()
                {
                    Metrics = new List<HddMetricDto>()
                    {
                       new HddMetricDto()
                       {
                           Id = 1,
                           Time = DateTimeOffset.Now,
                           Value = 99
                       }
                    }
                });

            var result = await _controller.GetMetricsByPeriod(request);
            var resultValue = ((OkObjectResult)result).Value as HddMetricResponse;

            _mockMediator.Verify(mediator => mediator.Send(It.Is<HddMetricGetByPeriodQuery>(
                m => m.FromTime == request.FromTime && m.ToTime == request.ToTime),
                It.IsAny<CancellationToken>()), Times.Once);
            _mockMediator.Verify(mediator => mediator.Send(It.IsAny<HddMetricGetByPeriodQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Single(resultValue.Metrics);
            Assert.Equal(1, resultValue.Metrics[0].Id);
            Assert.Equal(99, resultValue.Metrics[0].Value);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsByPeriod_ShouldCall_LogInformation()
        {
            var request = new HddMetricGetByPeriodQuery()
            {
                FromTime = DateTimeOffset.Now.AddDays(-5),
                ToTime = DateTimeOffset.Now
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<HddMetricGetByPeriodQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new HddMetricResponse());
            var logText = $"Parameters: FromTime={request.FromTime} ToTime={request.ToTime}";

            _ = _controller.GetMetricsByPeriod(request);

            _mockLogger.Verify(
                x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Information),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().CompareTo(logText) == 0),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
        }
    }
}
