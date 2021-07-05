using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetricsAgent.Responses;
using MetricsAgent.DAL.Interfaces;

using AutoMapper;

namespace MetricsAgent.Features.Queries
{
    public class CpuMetricGetByPeriodQuery : IRequest<CpuMetricResponse>
    {
        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }

        public override string ToString()
        {
            return $"FromTime={FromTime} ToTime={ToTime}";
        }

        public class CpuMetricGetByPeriodQueryHandler : IRequestHandler<CpuMetricGetByPeriodQuery, CpuMetricResponse>
        {
            private readonly ICpuMetricsRepository _repository;
            private readonly IMapper _mapper;

            public CpuMetricGetByPeriodQueryHandler(ICpuMetricsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<CpuMetricResponse> Handle(CpuMetricGetByPeriodQuery request, CancellationToken cancellationToken)
            {
                var result = await Task.Run(() =>
                {
                    var metricsList = _repository.GetByPeriod(request.FromTime, request.ToTime);

                    var response = new CpuMetricResponse();

                    response.Metrics.AddRange(_mapper.Map<List<CpuMetricDto>>(metricsList));

                    return response;
                });

                return result;
            }
        }
    }
}
