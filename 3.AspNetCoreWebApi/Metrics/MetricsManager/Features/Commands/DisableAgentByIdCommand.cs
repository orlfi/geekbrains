using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetricsManager.DAL.Interfaces.Repositories;
using AutoMapper;

namespace MetricsManager.Features.Commands
{
    public class DisableAgentByIdCommand : IRequest
    {
        public int AgentId { get; set; }

        public override string ToString()
        {
            return $"{{AgentId={AgentId}}}";
        }

        public class DisableAgentByIdCommandHandler : IRequestHandler<DisableAgentByIdCommand>
        {
            private readonly IAgentsRepository _repository;

            public  DisableAgentByIdCommandHandler(IAgentsRepository repository) => _repository = repository;

            public async Task<Unit> Handle(DisableAgentByIdCommand request, CancellationToken cancellationToken)
            {
               await Task.Run(() =>
                {
                    _repository.DisableById(request.AgentId);
                });
                
                return Unit.Value;
            }
        }
    }
}
