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
            connection.Execute("INSERT INTO  Agents (AgentUrl, Enabled) VALUES (@AgentUrl, @Enabled)",
                new
                {
                    item.AgentUrl,
                    item.Enabled
                });
        }

        public void EnableById(int agentId)
        {
            SetStateById(agentId, true);
        }
        
        public void DisableById(int agentId)
        {
            SetStateById(agentId, false);
        }

        public IList<AgentInfo> GetRegistered()
        {
            var connection = _connectionManager.GetOpenedConnection();

            var result = connection.Query<AgentInfo>("SELECT AgentId, AgentUrl, Enabled FROM Agents").ToList();
            
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
