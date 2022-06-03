using Gymopedia.Core.Models;
using Gymopedia.Core.Sessions;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using MediatR;

namespace Gymopedia.Core.CoachWorkDays
{
    public class CreateCoachWorkDay
    {
        public record Request(DateTime From, DateTime Until, int MaxClient) : IRequest<Response>;

        public record Response(CoachWorkDayDto CoachWorkDays);

        public class Handler : IRequestHandler<Request, Response>
        {            
            private readonly ICoachWorkDayRepository _coachWorkDayRepository;
            public Handler(ICoachWorkDayRepository coachWorkDayRepository)
            {
                _coachWorkDayRepository = coachWorkDayRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
            
                var coachWorkDay = new CoachWorkDay(request.From);
                

                _coachWorkDayRepository.Add(coachWorkDay);
                await _coachWorkDayRepository.Commit(cancellationToken);

                return new Response(new CoachWorkDayDto
                {
                    Date = coachWorkDay.Date
                }) ;
            }
        }
    }
        
}