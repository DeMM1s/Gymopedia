using Gymopedia.Core.Models;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.ClientToSessions
{
    public class DeleteClientToSession
    {
        public record Request(int clientId, int sessionId) : IRequest<Response>;
        public record Response(ClientToSession? ClientToSession, string? Error = null);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IClientToSessionRepository _clientToSessionRepository;
            public Handler(IClientToSessionRepository clientToSessionRepository)
            {
                _clientToSessionRepository = clientToSessionRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var clientToSession = await _clientToSessionRepository.Delete(request.clientId, request.sessionId, cancellationToken);
                if (clientToSession == null)
                {
                    return new Response(null, Constants.ErrorMessage.Client.ClientNotFoundError);
                }
                return new Response(clientToSession);
            }
        }
    }
}
