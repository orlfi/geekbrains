using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsManager.DAL.Models;
using MetricsManager.Responses.Metrics;
using MetricsManager.Responses.Agents;
using MetricsManager.Features.Commands.Agents;
using Core.Responses;

namespace MetricsManager.Features.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AgentInfo, AgentInfoDto>();
            CreateMap<RegisterAgentCommand, AgentInfo>();

            CreateMap<CpuMetric, CpuMetricDto>();
            CreateMap<AgentCpuMetricDto, CpuMetric>();

            CreateMap<DotNetMetric, DotNetMetricDto>();
            CreateMap<AgentDotNetMetricDto, DotNetMetric>();

            CreateMap<HddMetric, HddMetricDto>();
            CreateMap<AgentHddMetricDto, HddMetric>();

            CreateMap<NetworkMetric, NetworkMetricDto>();
            CreateMap<AgentNetworkMetricDto, NetworkMetric>();

            CreateMap<RamMetric, RamMetricDto>();
            CreateMap<AgentRamMetricDto, RamMetric>();
        }
    }
}
