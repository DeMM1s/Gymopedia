using Gymopedia.Domain.DtoModels;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.Sessions
{
    public class GetNearestClient
    {
        public record Request(long chatId) : IRequest<Response>;
        public record Response(SessionDto? Session, string? Error = null);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ISessionRepository _sessionRepository;
            public Handler(ISessionRepository sessionRepository)
            {
                _sessionRepository = sessionRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var session = await _sessionRepository.GetNearestClient(request.chatId, cancellationToken);

                return new Response(session);
            }
        }
    }
}
