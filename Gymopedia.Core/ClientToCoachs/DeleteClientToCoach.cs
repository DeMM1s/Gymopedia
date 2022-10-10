using Gymopedia.Core.Models;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.ClientToCoachs
{
    public class DeleteClientToCoach
    {
        public record Request(int clientId, int coachId) : IRequest<Response>;
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

                var clientToCoach = await _clientToCoachRepository.Delete(request.clientId, request.coachId, cancellationToken);
                if (clientToCoach == null)
                {
                    return new Response(null, Constants.ErrorMessage.Client.ClientNotFoundError);
                }
                return new Response(clientToCoach);
            }
        }
    }
}
