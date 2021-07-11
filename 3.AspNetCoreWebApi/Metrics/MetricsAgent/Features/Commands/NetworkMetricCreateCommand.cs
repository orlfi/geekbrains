using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetricsAgent.Responses;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using AutoMapper;

namespace MetricsAgent.Features.Commands
{
    public class NetworkMetricCreateCommand : IRequest
    {
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }

        public override string ToString()
        {
            return $"{{Value={Value}, Time={Time}}}";
        }

        public class NetworkMetricCreateCommandHandler : IRequestHandler<NetworkMetricCreateCommand>
        {
            private readonly INetworkMetricsRepository _repository;
            private readonly IMapper _mapper;

            public  NetworkMetricCreateCommandHandler(INetworkMetricsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(NetworkMetricCreateCommand request, CancellationToken cancellationToken)
            {
               await Task.Run(() =>
                {
                    _repository.Create(_mapper.Map<NetworkMetric>(request));
                });
                return Unit.Value;
            }
        }
    }
}
