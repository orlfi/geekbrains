using System;
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
using MetricsAgent.Features.Commands;

namespace MetricsAgentTests
{
    public class NetworkMetricsControllerUnitTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<NetworkMetricsController>> _mockLogger;
        private readonly NetworkMetricsController _controller;

        public NetworkMetricsControllerUnitTests()
        {
            _mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            _mockMediator = new Mock<IMediator>();
            _controller = new NetworkMetricsController(_mockLogger.Object, _mockMediator.Object);
        }

        [Fact]
        public void GetMetricsByPeriod_ReturnOk()
        {
            var request = new NetworkMetricGetByPeriodQuery()
            {
                FromTime = DateTimeOffset.Now.AddDays(-5),
                ToTime = DateTimeOffset.Now
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<NetworkMetricGetByPeriodQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new NetworkMetricResponse());

            var result = _controller.GetMetricsByPeriod(request);

            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void GetMetricsByPeriod_ShouldCall_MediatorSend()
        {
            var request = new NetworkMetricGetByPeriodQuery()
            {
                FromTime = DateTimeOffset.Now.AddDays(-5),
                ToTime = DateTimeOffset.Now
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<NetworkMetricGetByPeriodQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new NetworkMetricResponse());

            _ = _controller.GetMetricsByPeriod(request);

            _mockMediator.Verify(mediator =>  mediator.Send(It.IsAny<NetworkMetricGetByPeriodQuery>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce());
        }
        
        [Fact]
        public void GetMetricsByPeriod_ShouldCall_LogInformation()
        {
            var request = new NetworkMetricGetByPeriodQuery()
            {
                FromTime = DateTimeOffset.Now.AddDays(-5),
                ToTime = DateTimeOffset.Now
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<NetworkMetricGetByPeriodQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new NetworkMetricResponse());
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

        [Fact]
        public void Create_ReturnOk()
        {
            var request = new NetworkMetricCreateCommand
            {
                Value = 50,
                Time = DateTimeOffset.Now.AddDays(-5)
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<NetworkMetricCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Unit.Value);

            var result = _controller.Create(request);
            
            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }
        
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            var request = new NetworkMetricCreateCommand
            {
                Value = 50,
                Time = DateTimeOffset.Now.AddDays(-5)
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<NetworkMetricCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Unit.Value);

            var result = _controller.Create(request);

            _mockMediator.Verify(mediator =>  mediator.Send(It.IsAny<NetworkMetricCreateCommand>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce());
        }

        [Fact]
        public void Create_ShouldNotCall_Create_From_Repository_If_Value_Not_Between_0_100()
        {
            var request = new NetworkMetricCreateCommand
            {
                Value = 500,
                Time = DateTimeOffset.Now.AddDays(-5)
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<NetworkMetricCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Unit.Value);

            var result = _controller.Create(request);

            _mockMediator.Verify(mediator =>  mediator.Send(It.IsAny<NetworkMetricCreateCommand>(), It.IsAny<CancellationToken>()), Times.Never());
        }

        [Fact]
        public void Create_ShouldCall_LogInformation()
        {
            var request = new NetworkMetricCreateCommand
            {
                Value = 50,
                Time = DateTimeOffset.Now.AddDays(-5)
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<NetworkMetricCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Unit.Value);
            var logText = $"Parameters: request={request}";

            var result = _controller.Create(request);

            _mockLogger.Verify(
                logger => logger.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().CompareTo(logText) == 0),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
        }
    }
}
