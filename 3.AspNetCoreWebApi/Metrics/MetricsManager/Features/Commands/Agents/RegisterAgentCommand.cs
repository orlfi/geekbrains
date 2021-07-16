using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetricsManager.DAL.Interfaces.Repositories;
using MetricsManager.DAL.Models;
using MetricsManager.Responses.Agents;
using AutoMapper;

namespace MetricsManager.Features.Commands.Agents
{
    public class RegisterAgentCommand : IRequest<AgentInfoDto>
    {
        public Uri AgentUrl { get; set; }

        public bool IsEnabled { get; set; }

        
        public override string ToString()
        {
            return $"{{ AgentAddress={AgentUrl}, Enabled={IsEnabled}}}";
        }

        public class RegisterAgentCommandHandler : IRequestHandler<RegisterAgentCommand, AgentInfoDto>
        {
            private readonly IAgentsRepository _repository;
            private readonly IMapper _mapper;

            public  RegisterAgentCommandHandler(IAgentsRepository repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

            public async Task<AgentInfoDto> Handle(RegisterAgentCommand request, CancellationToken cancellationToken)
            {
               var result = await Task.Run(() =>
                {
                    return _mapper.Map<AgentInfoDto>(_repository.Create(_mapper.Map<AgentInfo>(request)));
                });
                
                return result;
            }
        }
    }
}
