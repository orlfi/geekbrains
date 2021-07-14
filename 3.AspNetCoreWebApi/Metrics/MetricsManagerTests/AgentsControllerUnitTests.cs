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
using MetricsManager.Features.Queries.Agents;
using MetricsManager.Responses.Agents;
using MetricsManager.Features.Commands;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<AgentsController>> _mockLogger;
        private readonly AgentsController _controller;

        public AgentsControllerUnitTests()
        {
            _mockLogger = new Mock<ILogger<AgentsController>>();
            _mockMediator = new Mock<IMediator>();
            _controller = new AgentsController(_mockLogger.Object, _mockMediator.Object);
        }

        [Fact]
        public async Task GetRegisteredAgents_ReturnOk()
        {
            Uri url = new Uri("https://localhost:5201/");
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<GetRegisteredAgentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AgentInfoResponse()
                {
                    Agents = new List<AgentInfoDto>()
                    {
                       new AgentInfoDto()
                       {
                           AgentId = 2,
                           AgentUrl =url,
                           IsEnabled = false
                       }
                    }
                });

            var result = await _controller.GetRegisteredAgents();
            var resultValue = ((OkObjectResult)result).Value as AgentInfoResponse;

            _mockMediator.Verify(mediator => mediator.Send(It.IsAny<GetRegisteredAgentsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Single(resultValue.Agents);
            Assert.Equal(2, resultValue.Agents[0].AgentId);
            Assert.Equal(url, resultValue.Agents[0].AgentUrl);
            Assert.False(resultValue.Agents[0].IsEnabled);
            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void GetRegisteredAgents_ShouldCall_LogInformation()
        {

            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<GetRegisteredAgentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AgentInfoResponse());
            var logText = "Getting registered Agents List";

            _ = _controller.GetRegisteredAgents();

            _mockLogger.Verify(
                x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Debug),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().CompareTo(logText) == 0),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
        }

        [Fact]
        public async Task RegisterAgent_ReturnOk()
        {
            Uri url = new Uri("https://localhost:5201/");
            var command = new RegisterAgentCommand()
            {
                AgentUrl = url,
                IsEnabled = true
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<RegisterAgentCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            var result = await _controller.RegisterAgent(command);

            _mockMediator.Verify(mediator => mediator.Send(It.Is<RegisterAgentCommand>(
                m => m.AgentUrl == command.AgentUrl && m.IsEnabled == command.IsEnabled),
                It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void RegisterAgent_ShouldCall_LogInformation()
        {
            Uri url = new Uri("https://localhost:5201/");
            var command = new RegisterAgentCommand()
            {
                AgentUrl = url,
                IsEnabled = true
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<RegisterAgentCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);
            var logText = $"Register Agent Parameters: command={command}";

            _ = _controller.RegisterAgent(command);

            _mockLogger.Verify(
                x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Information),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().CompareTo(logText) == 0),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
        }

        [Fact]
        public async Task EnableAgentById_ReturnOk()
        {
            var command = new EnableAgentByIdCommand()
            {
                AgentId = 1
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<EnableAgentByIdCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            var result = await _controller.EnableAgentById(command);

            _mockMediator.Verify(mediator => mediator.Send(It.Is<EnableAgentByIdCommand>(
                m => m.AgentId == command.AgentId),
                It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgentById_ShouldCall_LogInformation()
        {
            var command = new EnableAgentByIdCommand()
            {
                AgentId = 1
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<EnableAgentByIdCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);
            var logText = $"EnableAgentById Parameters: command={command}";

            _ = _controller.EnableAgentById(command);

            _mockLogger.Verify(
                x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Information),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().CompareTo(logText) == 0),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
        }

                [Fact]
        public async Task DisableAgentById_ReturnOk()
        {
            var command = new DisableAgentByIdCommand()
            {
                AgentId = 1
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<DisableAgentByIdCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            var result = await _controller.DisableAgentById(command);

            _mockMediator.Verify(mediator => mediator.Send(It.Is<DisableAgentByIdCommand>(
                m => m.AgentId == command.AgentId),
                It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgentById_ShouldCall_LogInformation()
        {
            var command = new DisableAgentByIdCommand()
            {
                AgentId = 1
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<DisableAgentByIdCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);
            var logText = $"DisableAgentById Parameters: command={command}";

            _ = _controller.DisableAgentById(command);

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
