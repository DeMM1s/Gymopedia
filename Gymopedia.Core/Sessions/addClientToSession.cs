using Gymopedia.Domain.DtoModels;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;


namespace Gymopedia.Core.Sessions
{
    public class AddClientToSession
    {
        public record Request(Client client, int sessionId) : IRequest<Response>;
        public record Response(Session? session, string? Error = null);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ISessionRepository _sessionRepository;
            public Handler(ISessionRepository sessionRepository)
            {
                _sessionRepository = sessionRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var session = await _sessionRepository.Get(request.sessionId, cancellationToken);
                if (session == null)
                {
                    return new Response(null, Constants.ErrorMessage.Session.SessionNotFoundError);
                }
                //session.Clients.Add(request.client);
                await _sessionRepository.Commit(cancellationToken);

                return new Response(session);
            }
        }
    }
}
