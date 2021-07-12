using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System.Linq;
using Dapper;

namespace MetricsManager.DAL.Repositories
{
    public class AgentsRepository : IAgentsRepository
    {
        private readonly IConnectionManager _connectionManager;
        public AgentsRepository(IConnectionManager connectionManager) => _connectionManager = connectionManager;

        public void Create(AgentInfo item)
        {
            var connection = _connectionManager.GetOpenedConnection();
            connection.Execute("CREATE Agents SET (AgentAddress, Enabled) VALUES (@AgentAddress, @Enabled)",
                new
                {
                    item.AgentAddress,
                    item.Enabled
                });
        }

        public void DisableById(int agentId)
        {
            SetStateById(agentId, false);
        }

        public void EnableById(int agentId)
        {
            SetStateById(agentId, true);
        }

        public IList<AgentInfo> GetRegistered()
        {
            var connection = _connectionManager.GetOpenedConnection();

            var result = connection.Query<AgentInfo>("SELECT AgentId, AgentAddress, Enabled FROM Agents").ToList();
            
            return result;
        }

        public IList<AgentInfo> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = _connectionManager.GetOpenedConnection();

            var result = connection.Query<AgentInfo>("SELECT Id, AgentId, Value, Time FROM CpuMetrics WHERE Time >= @FromTime AND Time <= @ToTime",
                new
                {
                    FromTime = fromTime,
                    ToTime = toTime
                }).ToList();
            
            return result;
        }

        public IList<AgentInfo> GetByPeriodFormAgent(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = _connectionManager.GetOpenedConnection();

            var result = connection.Query<AgentInfo>("SELECT Id, AgentId, Value, Time FROM CpuMetrics WHERE Time >= @FromTime AND Time <= @ToTime AND AgentId = @AgentId",
                new
                {
                    AgentId = agentId,
                    FromTime = fromTime,
                    ToTime = toTime
                }).ToList();

            return result;
        }

        private void SetStateById(int agentId, bool state)
        {
            var connection = _connectionManager.GetOpenedConnection();
            connection.Execute("UPDATE Agents SET Enabled = @state WHERE AgentId = @agentId",
                new
                {
                    agentId,
                    state
                });
        }
    }
}
