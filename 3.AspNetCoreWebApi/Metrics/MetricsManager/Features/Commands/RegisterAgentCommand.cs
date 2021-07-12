using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using AutoMapper;

namespace MetricsManager.Features.Commands
{
    public class RegisterAgentCommand : IRequest
    {
        public int AgentId { get; set; }

        public Uri AgentAddress { get; set; }
        public int Enabled { get; set; }

        public override string ToString()
        {
            return $"{{AgentId={AgentId}, AgentAddress={AgentAddress}, Enabled={Enabled}}}";
        }

        public class RegisterAgentCommandHandler : IRequestHandler<RegisterAgentCommand>
        {
            private readonly IAgentsRepository _repository;
            private readonly IMapper _mapper;

            public  RegisterAgentCommandHandler(IAgentsRepository repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

            public async Task<Unit> Handle(RegisterAgentCommand request, CancellationToken cancellationToken)
            {
               await Task.Run(() =>
                {
                    _repository.Create(_mapper.Map<AgentInfo>(request));
                });
                
                return Unit.Value;
            }
        }
    }
}
