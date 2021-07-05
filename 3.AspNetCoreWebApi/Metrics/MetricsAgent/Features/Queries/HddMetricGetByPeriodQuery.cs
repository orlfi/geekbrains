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
    public class HddMetricGetByPeriodQuery : IRequest<HddMetricResponse>
    {
        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }

        public override string ToString()
        {
            return $"FromTime={FromTime} ToTime={ToTime}";
        }

        public class HddMetricGetByPeriodQueryHandler : IRequestHandler<HddMetricGetByPeriodQuery, HddMetricResponse>
        {
            private readonly IHddMetricsRepository _repository;
            private readonly IMapper _mapper;

            public HddMetricGetByPeriodQueryHandler(IHddMetricsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<HddMetricResponse> Handle(HddMetricGetByPeriodQuery request, CancellationToken cancellationToken)
            {
                var result = await Task.Run(() =>
                {
                    var metricsList = _repository.GetByPeriod(request.FromTime, request.ToTime);

                    var response = new HddMetricResponse();

                    response.Metrics.AddRange(_mapper.Map<List<HddMetricDto>>(metricsList));

                    return response;
                });

                return result;
            }
        }
    }
}
