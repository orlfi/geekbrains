using AutoMapper;
using MetricsAgent.DAL.Models;
using Core.Responses;

namespace MetricsAgent.Features.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, AgentCpuMetricDto>();

            CreateMap<RamMetric, AgentRamMetricDto>();

            CreateMap<NetworkMetric, AgentNetworkMetricDto>();

            CreateMap<HddMetric, AgentHddMetricDto>();

            CreateMap<DotNetMetric, AgentDotNetMetricDto>();
        }
    }
}
