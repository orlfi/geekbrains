using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;

namespace MetricsAgent.Features.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>();
            CreateMap<CpuMetricCreateRequest, CpuMetric>();

            CreateMap<RamMetric, RamMetricDto>();
            CreateMap<RamMetricCreateRequest, RamMetric>();

            CreateMap<NetworkMetric, NetworkMetricDto>();
            CreateMap<NetworkMetricCreateRequest, NetworkMetric>();

            CreateMap<HddMetric, HddMetricDto>();
            CreateMap<HddMetricCreateRequest, HddMetric>();

            CreateMap<DotNetMetric, DotNetMetricDto>();
            CreateMap<DotNetMetricCreateRequest, DotNetMetric>();
        }
    }
}
