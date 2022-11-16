using Gymopedia.Domain.DtoModels;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.ClientToSessions
{
    public class DeleteClientToSession
    {
        public record Request(long clientId, int sessionId) : IRequest<Response>;
        public record Response(long clientId, string? Error = null);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IClientToSessionRepository _clientToSessionRepository;
            public Handler(IClientToSessionRepository clientToSessionRepository)
            {
                _clientToSessionRepository = clientToSessionRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                await _clientToSessionRepository.Delete(request.clientId, request.sessionId, cancellationToken);
                return new Response(request.clientId);
            }
        }
    }
}
