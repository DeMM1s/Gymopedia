using Gymopedia.Domain.DtoModels;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.ClientToCoachs
{
    public class SubscribeToCoach
    {
        public record Request(long clientId, long coachId) : IRequest<Response>;
        public record Response(ClientToCoach? ClientToCoach, string? Error = null);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IClientToCoachRepository _clientToCoachRepository;
            public Handler(IClientToCoachRepository clientToCoachRepository)
            {
                _clientToCoachRepository = clientToCoachRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var clientToCoach = new ClientToCoach { ClientId = request.clientId, CoachId = request.coachId };
                _clientToCoachRepository.Add(clientToCoach);
                await _clientToCoachRepository.Commit(cancellationToken);

                return new Response(clientToCoach);
            }
        }
    }
}
