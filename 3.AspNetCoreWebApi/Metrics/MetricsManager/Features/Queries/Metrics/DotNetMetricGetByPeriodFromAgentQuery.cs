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
    public class DotNetMetricGetByPeriodFromAgentQuery : IRequest<DotNetMetricResponse>
    {
        public int AgentId { get; set; }

        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }

        public override string ToString()
        {
            return $"FromTime={FromTime} ToTime={ToTime}";
        }

        public class DotNetMetricGetByPeriodQueryFromAgentHandler : IRequestHandler<DotNetMetricGetByPeriodFromAgentQuery, DotNetMetricResponse>
        {
            private readonly IDotNetMetricsRepository _repository;
            private readonly IMapper _mapper;

            public DotNetMetricGetByPeriodQueryFromAgentHandler(IDotNetMetricsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<DotNetMetricResponse> Handle(DotNetMetricGetByPeriodFromAgentQuery request, CancellationToken cancellationToken)
            {
                var result = await Task.Run(() =>
                {
                    var metricsList = _repository.GetByPeriodFormAgent(request.AgentId, request.FromTime, request.ToTime);

                    var response = new DotNetMetricResponse();

                    response.Metrics.AddRange(_mapper.Map<List<DotNetMetricDto>>(metricsList));

                    return response;
                });

                return result;
            }
        }
    }
}
