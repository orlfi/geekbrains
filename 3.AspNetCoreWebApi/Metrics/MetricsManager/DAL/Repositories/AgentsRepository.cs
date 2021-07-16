using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsManager.DAL.Interfaces.Repositories;
using MetricsManager.DAL.Models;
using System.Linq;
using Dapper;

namespace MetricsManager.DAL.Repositories
{
    public class AgentsRepository : IAgentsRepository
    {
        private readonly IConnectionManager _connectionManager;
        public AgentsRepository(IConnectionManager connectionManager) => _connectionManager = connectionManager;

        public AgentInfo Create(AgentInfo agentInfo)
        {
            var connection = _connectionManager.GetOpenedConnection();
            connection.Execute("INSERT INTO  Agents (AgentUrl, IsEnabled) VALUES (@AgentUrl, @IsEnabled)",
                new
                {
                    agentInfo.AgentUrl,
                    agentInfo.IsEnabled
                });
                agentInfo.AgentId = (int)connection.LastInsertRowId;
                return agentInfo;
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

            var result = connection.Query<AgentInfo>("SELECT AgentId, AgentUrl, IsEnabled FROM Agents").ToList();
            
            return result;
        }

        private void SetStateById(int agentId, bool state)
        {
            var connection = _connectionManager.GetOpenedConnection();
            connection.Execute("UPDATE Agents SET IsEnabled = @state WHERE AgentId = @agentId",
                new
                {
                    agentId,
                    state
                });
        }
    }
}
