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
    public class DotNetMetricCreateCommand : IRequest
    {
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }

        public override string ToString()
        {
            return $"{{Value={Value}, Time={Time}}}";
        }

        public class DotNetMetricCreateCommandHandler : IRequestHandler<DotNetMetricCreateCommand>
        {
            private readonly IDotNetMetricsRepository _repository;
            private readonly IMapper _mapper;

            public  DotNetMetricCreateCommandHandler(IDotNetMetricsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(DotNetMetricCreateCommand request, CancellationToken cancellationToken)
            {
               await Task.Run(() =>
                {
                    _repository.Create(_mapper.Map<DotNetMetric>(request));
                });
                return Unit.Value;
            }
        }
    }
}
