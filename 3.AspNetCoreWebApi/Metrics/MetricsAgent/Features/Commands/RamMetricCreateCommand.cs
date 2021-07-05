using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using AutoMapper;

namespace MetricsAgent.Features.Commands
{
    public class RamMetricCreateCommand : IRequest
    {
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }

        public override string ToString()
        {
            return $"{{Value={Value}, Time={Time}}}";
        }

        public class RamMetricCreateCommandHandler : IRequestHandler<RamMetricCreateCommand>
        {
            private readonly IRamMetricsRepository _repository;
            private readonly IMapper _mapper;

            public  RamMetricCreateCommandHandler(IRamMetricsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(RamMetricCreateCommand request, CancellationToken cancellationToken)
            {
               await Task.Run(() =>
                {
                    _repository.Create(_mapper.Map<RamMetric>(request));
                });
                return Unit.Value;
            }
        }
    }
}
