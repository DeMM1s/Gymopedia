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

            private readonly ISessionRepository _sessionRepository;
            public Handler(ICoachWorkDayRepository coachWorkDayRepository)
            {
                _coachWorkDayRepository = coachWorkDayRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
            
                var coachWorkDay = new CoachWorkDay(request.From);

                for (DateTime t = request.From; t < request.Until; t.AddHours(1))
                {
                    var reqiestSession = new CreateSession.Request(
                        t,
                        t.AddHours(1),
                        request.MaxClient,
                        coachWorkDay.Id);

                    var handler = new CreateSession.Handler(_sessionRepository);
                    var createSessionResponse = await handler.Handle(reqiestSession, cancellationToken);

                    
                    coachWorkDay.Sessions.Add(
                        new Session(createSessionResponse.Session.From,
                        createSessionResponse.Session.Until,
                        createSessionResponse.Session.MaxClient,
                        createSessionResponse.Session.CoachWorkDayId));
                }


                _coachWorkDayRepository.Add(coachWorkDay);
                await _coachWorkDayRepository.Commit(cancellationToken);

                return new Response(new CoachWorkDayDto
                {
                    Date = coachWorkDay.Date,
                    Sessions = coachWorkDay.Sessions,
                }) ;
            }
        }
    }
        
}