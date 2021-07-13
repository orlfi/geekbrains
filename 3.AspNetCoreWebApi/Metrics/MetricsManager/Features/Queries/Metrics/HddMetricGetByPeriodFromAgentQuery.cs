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
    public class HddMetricGetByPeriodFromAgentQuery : IRequest<HddMetricResponse>
    {
        public int AgentId { get; set; }

        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }

        public override string ToString()
        {
            return $"FromTime={FromTime} ToTime={ToTime}";
        }

        public class HddMetricGetByPeriodQueryFromAgentHandler : IRequestHandler<HddMetricGetByPeriodFromAgentQuery, HddMetricResponse>
        {
            private readonly IHddMetricsRepository _repository;
            private readonly IMapper _mapper;

            public HddMetricGetByPeriodQueryFromAgentHandler(IHddMetricsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<HddMetricResponse> Handle(HddMetricGetByPeriodFromAgentQuery request, CancellationToken cancellationToken)
            {
                var result = await Task.Run(() =>
                {
                    var metricsList = _repository.GetByPeriodFormAgent(request.AgentId, request.FromTime, request.ToTime);

                    var response = new HddMetricResponse();

                    response.Metrics.AddRange(_mapper.Map<List<HddMetricDto>>(metricsList));

                    return response;
                });

                return result;
            }
        }
    }
}
