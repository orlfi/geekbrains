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
        public bool Enabled { get; set; }
    }
}
