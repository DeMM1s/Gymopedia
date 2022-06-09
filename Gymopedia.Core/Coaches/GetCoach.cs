using Gymopedia.Core.Models;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.Coaches
{
    public class GetCoach
    {
        public record Request(int ID) : IRequest<Response>;
        public record Response(CoachDto? Coach, string? Error = null);

        public class Heandler : IRequestHandler<Request, Response>
        {
            private readonly ICoachRepository _coachRepository;
            public Heandler(ICoachRepository coachRepository)
            {
                _coachRepository = coachRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var coach = await _coachRepository.Get(request.ID, cancellationToken);
                if(coach == null)
                {
                    return new Response(null, Constants.ErrorMessage.Coach.CoachNotFoundError);
                }
                return new Response(new CoachDto
                {
                    Name = coach.Name,
                    Id = coach.Id,
                    Calendar = coach.Calendar,
                    Clients = coach.Clients,
                });
            }
        }
    }
}
