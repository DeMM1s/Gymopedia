using Gymopedia.Core.Models;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.Sessions
{
    public class EditSession
    {
        public record Request(Session Session) : IRequest<Response>;
        public record Response(Session? Session, string? Error = null);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ISessionRepository _sessionRepository;
            public Handler(ISessionRepository sessionRepository)
            {
                _sessionRepository = sessionRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var session = await _sessionRepository.Get(request.Session.Id, cancellationToken);
                if (session == null)
                {
                    return new Response(null, Constants.ErrorMessage.Session.SessionNotFoundError);
                }
                return new Response(session);
            }
        }
    }
}
