using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Interfaces.Repositories
{
    public interface IAgentsRepository
    {
        AgentInfo Create(AgentInfo item);

        IList<AgentInfo> GetRegistered();

        void EnableById(int agentId);

        void DisableById(int agentId);
    }
}
