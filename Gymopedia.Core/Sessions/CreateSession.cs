﻿using Gymopedia.Core.Models;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using MediatR;

namespace Gymopedia.Core.Sessions
{
    public class CreateSession
    {
        public record Request(string Name) : IRequest<Response>;

        public record Response(SessionDto session);
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ISessionRepository _sessionRepository;
            public Handler(ISessionRepository sessionRepository)
            {
                _sessionRepository = sessionRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var coach = new Coach(
                    request.Name);

                _coachRepository.Add(coach);
                await _coachRepository.Commit(cancellationToken);

                return new Response(new CoachDto
                {
                    Name = request.Name
                });
            }

        }
    }
}
