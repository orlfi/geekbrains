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
    public class RamMetricGetByPeriodQuery : IRequest<AgentRamMetricResponse>
    {
        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }

        public override string ToString()
        {
            return $"FromTime={FromTime} ToTime={ToTime}";
        }

        public class RamMetricGetByPeriodQueryHandler : IRequestHandler<RamMetricGetByPeriodQuery, AgentRamMetricResponse>
        {
            private readonly IRamMetricsRepository _repository;
            private readonly IMapper _mapper;

            public RamMetricGetByPeriodQueryHandler(IRamMetricsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<AgentRamMetricResponse> Handle(RamMetricGetByPeriodQuery request, CancellationToken cancellationToken)
            {
                var result = await Task.Run(() =>
                {
                    var metricsList = _repository.GetByPeriod(request.FromTime, request.ToTime);

                    var response = new AgentRamMetricResponse();

                    response.Metrics.AddRange(_mapper.Map<List<AgentRamMetricDto>>(metricsList));

                    return response;
                });

                return result;
            }
        }
    }
}
