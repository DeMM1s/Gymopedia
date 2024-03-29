﻿using Gymopedia.Domain.DtoModels;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using MediatR;

namespace Gymopedia.Core.Coaches
{
    public class CreateCoach
    {
        public record Request(string Name,string FullName, long ChatId) : IRequest<Response>;

        public record Response(Coach Coach);
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ICoachRepository _coachRepository;
            public Handler(ICoachRepository coachRepository)
            {
                _coachRepository = coachRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var coach = new Coach(
                    request.Name, request.FullName, request.ChatId);

                _coachRepository.Add(coach);
                await _coachRepository.Commit(cancellationToken);

                return new Response(coach);
            }

        }
    }
}
