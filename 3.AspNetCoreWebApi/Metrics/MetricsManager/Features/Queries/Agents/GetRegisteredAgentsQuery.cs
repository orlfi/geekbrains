using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetricsManager.Responses.Agents;
using MetricsManager.DAL.Interfaces;

using AutoMapper;

namespace MetricsManager.Features.Queries.Metrics
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
                    var metricsList = _repository.GetRegistered();

                    var response = new AgentInfoResponse();

                    response.Metrics.AddRange(_mapper.Map<List<AgentInfoDto>>(metricsList));

                    return response;
                });

                return result;
            }
        }
    }
}
