using Gymopedia.Core.Models;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.Coaches
{
    public class DeleteCoach
    {
        public record Request(int ID) : IRequest<Response>;
        public record Response(CoachDto? Coach, string? Error = null);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ICoachRepository _coachRepository;
            public Handler(ICoachRepository coachRepository)
            {
                _coachRepository = coachRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var coach = await _coachRepository.Delete(request.ID, cancellationToken);
                if (coach == null)
                {
                    return new Response(null, Constants.ErrorMessage.Client.ClientNotFoundError);
                }
                return new Response(new CoachDto
                {
                    Name = coach.Name,
                    Id = coach.Id
                });
            }
        }
    }
}