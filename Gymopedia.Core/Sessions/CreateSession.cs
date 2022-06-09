using Gymopedia.Core.Models;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using MediatR;

namespace Gymopedia.Core.Sessions
{
    public class CreateSession
    {
        public record Request(DateTime From, DateTime Until, int MaxClient, int CoachWorkDayId) : IRequest<Response>;

        public record Response(SessionDto Session);
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ISessionRepository _sessionRepository;
            public Handler(ISessionRepository sessionRepository)
            {
                _sessionRepository = sessionRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var session = new Session(
                    request.From,
                    request.Until,
                    request.MaxClient,
                    request.CoachWorkDayId);

                _sessionRepository.Add(session);
                await _sessionRepository.Commit(cancellationToken);

                return new Response(new SessionDto
                {
                    From = request.From,
                    Until = request.Until,
                    MaxClient = request.MaxClient,
                    CoachWorkDayId = request.CoachWorkDayId,
                });
            }
        }
    }
}
