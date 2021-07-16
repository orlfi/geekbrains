using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Core.Responses;
using MetricsAgent.DAL.Interfaces;

using AutoMapper;

namespace MetricsAgent.Features.Queries
{
    public class DotNetMetricGetByPeriodQuery : IRequest<AgentDotNetMetricResponse>
    {
        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }

        public override string ToString()
        {
            return $"FromTime={FromTime} ToTime={ToTime}";
        }

        public class DotNetMetricGetByPeriodQueryHandler : IRequestHandler<DotNetMetricGetByPeriodQuery, AgentDotNetMetricResponse>
        {
            private readonly IDotNetMetricsRepository _repository;
            private readonly IMapper _mapper;

            public DotNetMetricGetByPeriodQueryHandler(IDotNetMetricsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<AgentDotNetMetricResponse> Handle(DotNetMetricGetByPeriodQuery request, CancellationToken cancellationToken)
            {
                var result = await Task.Run(() =>
                {
                    var metricsList = _repository.GetByPeriod(request.FromTime, request.ToTime);

                    var response = new AgentDotNetMetricResponse();

                    response.Metrics.AddRange(_mapper.Map<List<AgentDotNetMetricDto>>(metricsList));

                    return response;
                });

                return result;
            }
        }
    }
}
