using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.Responses;
using MetricsAgent.Features.Commands;

namespace MetricsAgent.Features.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>();
            CreateMap<CpuMetricCreateCommand, CpuMetric>();

            CreateMap<RamMetric, RamMetricDto>();
            CreateMap<RamMetricCreateCommand, RamMetric>();

            CreateMap<NetworkMetric, NetworkMetricDto>();
            CreateMap<NetworkMetricCreateCommand, NetworkMetric>();

            CreateMap<HddMetric, HddMetricDto>();
            CreateMap<HddMetricCreateCommand, HddMetric>();

            CreateMap<DotNetMetric, DotNetMetricDto>();
            CreateMap<DotNetMetricCreateCommand, DotNetMetric>();
        }
    }
}
