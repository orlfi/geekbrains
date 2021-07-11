using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using AutoMapper;

namespace MetricsAgent.Features.Commands
{
    public class CpuMetricCreateCommand : IRequest
    {
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }

        public override string ToString()
        {
            return $"{{Value={Value}, Time={Time}}}";
        }

        public class CpuMetricCreateCommandHandler : IRequestHandler<CpuMetricCreateCommand>
        {
            private readonly ICpuMetricsRepository _repository;
            private readonly IMapper _mapper;

            public  CpuMetricCreateCommandHandler(ICpuMetricsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(CpuMetricCreateCommand request, CancellationToken cancellationToken)
            {
               await Task.Run(() =>
                {
                    _repository.Create(_mapper.Map<CpuMetric>(request));
                });
                return Unit.Value;
            }
        }
    }
}
