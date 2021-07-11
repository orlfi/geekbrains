using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsManager.DAL.Models;
using MetricsManager.Responses;

namespace MetricsManager.Features.Mappers
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
