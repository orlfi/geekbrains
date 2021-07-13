using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetricsManager.Responses.Metrics;
using MetricsManager.DAL.Interfaces.Repositories;

using AutoMapper;

namespace MetricsManager.Features.Queries.Metrics
{
    public class RamMetricGetByPeriodQuery : IRequest<RamMetricResponse>
    {
        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }

        public override string ToString()
        {
            return $"FromTime={FromTime} ToTime={ToTime}";
        }

        public class RamMetricGetByPeriodQueryHandler : IRequestHandler<RamMetricGetByPeriodQuery, RamMetricResponse>
        {
            private readonly IRamMetricsRepository _repository;
            private readonly IMapper _mapper;

            public RamMetricGetByPeriodQueryHandler(IRamMetricsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<RamMetricResponse> Handle(RamMetricGetByPeriodQuery request, CancellationToken cancellationToken)
            {
                var result = await Task.Run(() =>
                {
                    var metricsList = _repository.GetByPeriod(request.FromTime, request.ToTime);

                    var response = new RamMetricResponse();

                    response.Metrics.AddRange(_mapper.Map<List<RamMetricDto>>(metricsList));

                    return response;
                });

                return result;
            }
        }
    }
}
