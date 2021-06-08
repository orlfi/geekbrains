using System;
using Xunit;
using MetricsManager.Controllers;
using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private AgentsController _controller;
        public AgentsControllerUnitTests()
        {
            _controller = new AgentsController();
        }

        [Fact]
        public void GetRegisteredAgents_ReturnsOK()
        {
            var result = _controller.GetRegisteredAgents();

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgentById_ReturnsOK()
        {
            int agentId = 1;

            var result = _controller.EnableAgentById(agentId);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgentById_ReturnsOK()
        {
            int agentId = 1;

            var result = _controller.DisableAgentById(agentId);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void RegisterAgent_ReturnsOK()
        {
            AgentInfo agentInfo = new AgentInfo()
            {
                AgentId = 1,
                AgentAdress = new Uri(@"http:\\192.168.1.2:5000")
            };

            var result = _controller.RegisterAgent(agentInfo);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
