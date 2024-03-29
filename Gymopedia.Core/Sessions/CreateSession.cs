﻿using Gymopedia.Domain.DtoModels;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using MediatR;

namespace Gymopedia.Core.Sessions
{
    public class CreateSession
    {
        public record Request(DateTime From, long CoachId) : IRequest<Response>;

        public record Response(Session Session);
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ISessionRepository _sessionRepository;
            public Handler(ISessionRepository sessionRepository)
            {
                _sessionRepository = sessionRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var session = new Session(request.From, request.CoachId);

                _sessionRepository.Add(session);
                await _sessionRepository.Commit(cancellationToken);

                return new Response(session);
            }
        }
    }
}
