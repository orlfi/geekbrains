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
    public class DotNetMetricGetByPeriodQuery : IRequest<DotNetMetricResponse>
    {
        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }

        public override string ToString()
        {
            return $"FromTime={FromTime} ToTime={ToTime}";
        }

        public class DotNetMetricGetByPeriodQueryHandler : IRequestHandler<DotNetMetricGetByPeriodQuery, DotNetMetricResponse>
        {
            private readonly IDotNetMetricsRepository _repository;
            private readonly IMapper _mapper;

            public DotNetMetricGetByPeriodQueryHandler(IDotNetMetricsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<DotNetMetricResponse> Handle(DotNetMetricGetByPeriodQuery request, CancellationToken cancellationToken)
            {
                var result = await Task.Run(() =>
                {
                    var metricsList = _repository.GetByPeriod(request.FromTime, request.ToTime);

                    var response = new DotNetMetricResponse();

                    response.Metrics.AddRange(_mapper.Map<List<DotNetMetricDto>>(metricsList));

                    return response;
                });

                return result;
            }
        }
    }
}
