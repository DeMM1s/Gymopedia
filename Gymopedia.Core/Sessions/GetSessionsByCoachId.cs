using Gymopedia.Domain.DtoModels;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.Sessions
{
    public class GetActualSessionsByCoachId
    {
        public record Request(long CoachId) : IRequest<Response>;
        public record Response(List<SessionDto>? SessionList, string? Error = null);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ISessionRepository _sessionRepository;
            public Handler(ISessionRepository sessionRepository)
            {
                _sessionRepository = sessionRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var list = await _sessionRepository.GetAllByCoachId(request.CoachId, cancellationToken);

                return new Response(list);
            }
        }
    }
}
