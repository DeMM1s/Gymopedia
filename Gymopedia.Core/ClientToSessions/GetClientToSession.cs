﻿using Gymopedia.Core.Models;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.ClientToSessions
{
    public class GetClientToSession
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
                var clientToSession = new ClientToSession { ClientId = request.clientId, SessionId = request.sessionId };

                await _clientToSessionRepository.Commit(cancellationToken);

                return new Response(clientToSession);
            }
        }
    }
}