using Gymopedia.Domain.DtoModels;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.Coaches
{
    public class GetCoach
    {
        public record Request(long ID) : IRequest<Response>;
        public record Response(Coach? Coach, string? Error = null);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ICoachRepository _coachRepository;
            public Handler(ICoachRepository coachRepository)
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
                return new Response(coach);
            }
        }
    }
}
