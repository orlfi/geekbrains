using System;
using System.Collections.Generic;
using Xunit;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Requests;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;

namespace MetricsAgentTests
{
    public class HddMetricsControllerUnitTests
    {
        private readonly Mock<IHddMetricsRepository> _mockRepository;
        private readonly Mock<ILogger<HddMetricsController>> _mockLogger;
        private readonly HddMetricsController _controller;

        public HddMetricsControllerUnitTests()
        {
            _mockRepository = new Mock<IHddMetricsRepository>();
            _mockLogger = new Mock<ILogger<HddMetricsController>>();
            _controller = new HddMetricsController(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetMetricsByPeriod_ReturnOk()
        {
            var fromTime = DateTimeOffset.Now.AddDays(-5);
            var toTime = DateTimeOffset.Now;
            var metrics = new List<HddMetric>
            {
                new HddMetric {Id = 1, Value = 10, Time = DateTimeOffset.Now.AddDays(-5)},
                new HddMetric {Id = 1, Value = 50, Time = DateTimeOffset.Now.AddDays(-4)}
            };
            _mockRepository.Setup(repository => repository.GetByPeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(metrics);

            var result = _controller.GetMetricsByPeriod(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsByPeriod_ShouldCall_LogInformation()
        {
            var fromTime = DateTimeOffset.Now.AddDays(-5);
            var toTime = DateTimeOffset.Now;
            var metrics = new List<HddMetric>
            {
                new HddMetric {Id = 1, Value = 10, Time = DateTimeOffset.Now.AddDays(-5)},
                new HddMetric {Id = 1, Value = 50, Time = DateTimeOffset.Now.AddDays(-4)}
            };
            _mockRepository.Setup(repository => repository.GetByPeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(metrics);
            var logText = $"Parameters: fromTime={fromTime} toTime={toTime}";

            _controller.GetMetricsByPeriod(fromTime, toTime);

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
            var request = new HddMetricCreateRequest
            {
                Value = 50,
                Time = DateTimeOffset.Now.AddDays(-5)
            };

            var result = _controller.Create(request);
            
            Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            var request = new HddMetricCreateRequest
            {
                Value = 50,
                Time = DateTimeOffset.Now.AddDays(-5)
            };

            var result = _controller.Create(request);

            _mockRepository.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtLeastOnce());
        }

        [Fact]
        public void Create_ShouldNotCall_Create_From_Repository_If_Value_Not_Between_0_100()
        {
            var request = new HddMetricCreateRequest
            {
                Value = 500,
                Time = DateTimeOffset.Now.AddDays(-5)
            };

            var result = _controller.Create(request);

            _mockRepository.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.Never());
        }

        [Fact]
        public void Create_ShouldCall_LogInformation()
        {
            var request = new HddMetricCreateRequest
            {
                Value = 50,
                Time = DateTimeOffset.Now.AddDays(-5)
            };
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
