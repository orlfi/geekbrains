using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Models
{
    public class AgentInfo
    {
        public int AgentId { get; set; }
        public Uri AgentUrl { get; set; }
        public bool IsEnabled { get; set; }

        public override string ToString()
        {
            return $"ID: {AgentId}, URL: {AgentUrl}, IsEnabled: {IsEnabled}";
        }
    }
}
