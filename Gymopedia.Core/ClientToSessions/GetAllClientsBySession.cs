using Gymopedia.Domain.DtoModels;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.ClientToSessions
{
    public class GetAllClientsBySession
    {
        public record Request(long sessionId) : IRequest<Response>;
        public record Response(List<ClientToSession> ClientToSessionList, string? Error = null);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IClientToSessionRepository _clientToSessionRepository;
            public Handler(IClientToSessionRepository clientToSessionRepository)
            {
                _clientToSessionRepository = clientToSessionRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var List = await _clientToSessionRepository.GetAllBySession(request.sessionId, cancellationToken);
                return new Response(List);
            }
        }
    }
}
