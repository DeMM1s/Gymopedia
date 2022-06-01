using Gymopedia.Core.Models;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using MediatR;

namespace Gymopedia.Core.CoachWorkDays
{
    public record Request(DateOnly date, TimeOnly from, TimeOnly until) : IRequest<Response>;

    public record Response(CoachWorkDayDto coachWorkDays);
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly ICoachWorkDayRepository _coachWorkDayRepository;
        public Handler(ICoachWorkDayRepository coachWorkDayRepository)
        {
            _coachWorkDayRepository = coachWorkDayRepository;
        }
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var coachWorkDay = new CoachWorkDay(request.date);
            for()
            {
                
            }

            _coachWorkDayRepository.Add(coachWorkDay);
            await _coachWorkDayRepository.Commit(cancellationToken);

            return new Response(new CoachWorkDayDto
            {
                Name = request.Name
            });
        }

    }
}
