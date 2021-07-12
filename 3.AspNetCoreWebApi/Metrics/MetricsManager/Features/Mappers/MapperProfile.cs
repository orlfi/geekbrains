using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsManager.DAL.Models;
using MetricsManager.Responses.Metrics;
using MetricsManager.Responses.Agents;
using MetricsManager.Features.Commands;

namespace MetricsManager.Features.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AgentInfo, AgentInfoDto>();
            CreateMap<RegisterAgentCommand, AgentInfo>();

            CreateMap<CpuMetric, CpuMetricDto>();

            CreateMap<RamMetric, RamMetricDto>();

            CreateMap<NetworkMetric, NetworkMetricDto>();

            CreateMap<HddMetric, HddMetricDto>();

            CreateMap<DotNetMetric, DotNetMetricDto>();
        }
    }
}
