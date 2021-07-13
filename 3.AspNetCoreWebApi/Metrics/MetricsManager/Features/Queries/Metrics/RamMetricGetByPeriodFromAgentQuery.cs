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
    public class RamMetricGetByPeriodFromAgentQuery : IRequest<RamMetricResponse>
    {
        public int AgentId { get; set; }

        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }

        public override string ToString()
        {
            return $"FromTime={FromTime} ToTime={ToTime}";
        }

        public class RamMetricGetByPeriodQueryFromAgentHandler : IRequestHandler<RamMetricGetByPeriodFromAgentQuery, RamMetricResponse>
        {
            private readonly IRamMetricsRepository _repository;
            private readonly IMapper _mapper;

            public RamMetricGetByPeriodQueryFromAgentHandler(IRamMetricsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<RamMetricResponse> Handle(RamMetricGetByPeriodFromAgentQuery request, CancellationToken cancellationToken)
            {
                var result = await Task.Run(() =>
                {
                    var metricsList = _repository.GetByPeriodFormAgent(request.AgentId, request.FromTime, request.ToTime);

                    var response = new RamMetricResponse();

                    response.Metrics.AddRange(_mapper.Map<List<RamMetricDto>>(metricsList));

                    return response;
                });

                return result;
            }
        }
    }
}
