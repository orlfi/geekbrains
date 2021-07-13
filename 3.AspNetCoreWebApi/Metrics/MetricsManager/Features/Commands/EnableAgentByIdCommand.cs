using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetricsManager.DAL.Interfaces.Repositories;
using AutoMapper;

namespace MetricsManager.Features.Commands
{
    public class EnableAgentByIdCommand : IRequest
    {
        public int AgentId { get; set; }

        public override string ToString()
        {
            return $"{{AgentId={AgentId}}}";
        }

        public class EnableAgentByIdCommandHandler : IRequestHandler<EnableAgentByIdCommand>
        {
            private readonly IAgentsRepository _repository;

            public  EnableAgentByIdCommandHandler(IAgentsRepository repository) => _repository = repository;

            public async Task<Unit> Handle(EnableAgentByIdCommand request, CancellationToken cancellationToken)
            {
               await Task.Run(() =>
                {
                    _repository.EnableById(request.AgentId);
                });
                
                return Unit.Value;
            }
        }
    }
}
