using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using MediatR;
using MetricsManager.Features.Queries.Metrics;
using MetricsManager.Responses.Metrics;

namespace MetricsManagerTests
{
    public class DotNetMetricsControllerUnitTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<DotNetMetricsController>> _mockLogger;
        private readonly DotNetMetricsController _controller;

        public DotNetMetricsControllerUnitTests()
        {
            _mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            _mockMediator = new Mock<IMediator>();
            _controller = new DotNetMetricsController(_mockLogger.Object, _mockMediator.Object);
        }

        [Fact]
        public async Task GetMetricsFromAgent_ReturnOk()
        {
            var request = new DotNetMetricGetByPeriodFromAgentQuery()
            {
                AgentId = 2,
                FromTime = DateTimeOffset.Now.AddDays(-1),
                ToTime = DateTimeOffset.Now
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<DotNetMetricGetByPeriodFromAgentQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DotNetMetricResponse()
                {
                    Metrics = new List<DotNetMetricDto>()
                    {
                       new DotNetMetricDto()
                       {
                           Id = 1,
                           AgentId = 2,
                           Time = DateTimeOffset.Now,
                           Value = 99
                       }
                    }
                });

            var result = await _controller.GetMetricsFromAgent(request);
            var resultValue = ((OkObjectResult)result).Value as DotNetMetricResponse;

            _mockMediator.Verify(mediator => mediator.Send(It.Is<DotNetMetricGetByPeriodFromAgentQuery>(
                m => m.AgentId == request.AgentId && m.FromTime == request.FromTime && m.ToTime == request.ToTime),
                It.IsAny<CancellationToken>()), Times.Once);
            Assert.Single(resultValue.Metrics);
            Assert.Equal(1, resultValue.Metrics[0].Id);
            Assert.Equal(request.AgentId, resultValue.Metrics[0].AgentId);
            Assert.Equal(99, resultValue.Metrics[0].Value);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        
        [Fact]
        public void GetMetricsFromAgent_ShouldCall_LogInformation()
        {
            var request = new DotNetMetricGetByPeriodFromAgentQuery()
            {
                AgentId = 2,
                FromTime = DateTimeOffset.Now.AddDays(-1),
                ToTime = DateTimeOffset.Now
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<DotNetMetricGetByPeriodFromAgentQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DotNetMetricResponse());
            var logText = $"DotNet GetMetricsFromAgent Parameters: FromTime={request.FromTime} ToTime={request.ToTime}";

            _ = _controller.GetMetricsFromAgent(request);

            _mockLogger.Verify(
                x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Information),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().CompareTo(logText) == 0),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
        }
        
        [Fact]
        public async Task GetMetricsFromAllCluster_ReturnOk()
        {
            var request = new DotNetMetricGetByPeriodQuery()
            {
                FromTime = DateTimeOffset.Now.AddDays(-1),
                ToTime = DateTimeOffset.Now
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<DotNetMetricGetByPeriodQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DotNetMetricResponse()
                {
                    Metrics = new List<DotNetMetricDto>()
                    {
                       new DotNetMetricDto()
                       {
                           Id = 1,
                           AgentId = 1,
                           Time = DateTimeOffset.Now,
                           Value = 99
                       }
                    }
                });

            var result = await _controller.GetMetricsFromAllCluster(request);
            var resultValue = ((OkObjectResult)result).Value as DotNetMetricResponse;

            _mockMediator.Verify(mediator => mediator.Send(It.Is<DotNetMetricGetByPeriodQuery>(
                m => m.FromTime == request.FromTime && m.ToTime == request.ToTime),
                It.IsAny<CancellationToken>()), Times.Once);
            Assert.Single(resultValue.Metrics);
            Assert.Equal(1, resultValue.Metrics[0].Id);
            Assert.Equal(1, resultValue.Metrics[0].AgentId);
            Assert.Equal(99, resultValue.Metrics[0].Value);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster_ShouldCall_LogInformation()
        {
            var request = new DotNetMetricGetByPeriodQuery()
            {
                FromTime = DateTimeOffset.Now.AddDays(-1),
                ToTime = DateTimeOffset.Now
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<DotNetMetricGetByPeriodQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new DotNetMetricResponse());
            var logText = $"DotNet GetMetricsFromAllCluster Parameters: FromTime={request.FromTime} ToTime={request.ToTime}";

            _ = _controller.GetMetricsFromAllCluster(request);

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
