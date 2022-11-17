using Gymopedia.Domain.DtoModels;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.ClientToCoachs
{
    public class GetClientToCoach
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
                var clientToCoach = await _clientToCoachRepository.Get(request.clientId, request.coachId, cancellationToken);
                return new Response(clientToCoach);
            }
        }
    }
}
