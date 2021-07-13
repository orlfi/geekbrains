using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.Responses;

namespace MetricsAgent.Features.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>();

            CreateMap<RamMetric, RamMetricDto>();

            CreateMap<NetworkMetric, NetworkMetricDto>();

            CreateMap<HddMetric, HddMetricDto>();

            CreateMap<DotNetMetric, DotNetMetricDto>();
        }
    }
}
