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
    public class NetworkMetricGetByPeriodQuery : IRequest<NetworkMetricResponse>
    {
        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }

        public override string ToString()
        {
            return $"FromTime={FromTime} ToTime={ToTime}";
        }

        public class NetworkMetricGetByPeriodQueryHandler : IRequestHandler<NetworkMetricGetByPeriodQuery, NetworkMetricResponse>
        {
            private readonly INetworkMetricsRepository _repository;
            private readonly IMapper _mapper;

            public NetworkMetricGetByPeriodQueryHandler(INetworkMetricsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<NetworkMetricResponse> Handle(NetworkMetricGetByPeriodQuery request, CancellationToken cancellationToken)
            {
                var result = await Task.Run(() =>
                {
                    var metricsList = _repository.GetByPeriod(request.FromTime, request.ToTime);

                    var response = new NetworkMetricResponse();

                    response.Metrics.AddRange(_mapper.Map<List<NetworkMetricDto>>(metricsList));

                    return response;
                });

                return result;
            }
        }
    }
}
