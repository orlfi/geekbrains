using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetricsManager.Responses.Agents;
using MetricsManager.DAL.Interfaces.Repositories;

using AutoMapper;

namespace MetricsManager.Features.Queries.Agents
{
    public class GetRegisteredAgentsQuery : IRequest<AgentInfoResponse>
    {
        public class GetRegisteredAgentsQueryHandler : IRequestHandler<GetRegisteredAgentsQuery, AgentInfoResponse>
        {
            private readonly IAgentsRepository _repository;
            private readonly IMapper _mapper;

            public GetRegisteredAgentsQueryHandler(IAgentsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<AgentInfoResponse> Handle(GetRegisteredAgentsQuery request, CancellationToken cancellationToken)
            {
                var result = await Task.Run(() =>
                {
                    var agentsList = _repository.GetRegistered();

                    var response = new AgentInfoResponse();

                    response.Agents.AddRange(_mapper.Map<List<AgentInfoDto>>(agentsList));

                    return response;
                });

                return result;
            }
        }
    }
}
