using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsManager.DAL.Models;
using MetricsManager.Responses.Metrics;
using MetricsManager.Responses.Agents;
using MetricsManager.Features.Commands;
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

            CreateMap<RamMetric, RamMetricDto>();

            CreateMap<NetworkMetric, NetworkMetricDto>();

            CreateMap<HddMetric, HddMetricDto>();

            CreateMap<DotNetMetric, DotNetMetricDto>();
        }
    }
}
