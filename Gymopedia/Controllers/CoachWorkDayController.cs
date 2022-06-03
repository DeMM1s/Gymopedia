using Microsoft.AspNetCore.Mvc;
using Gymopedia.Core.Models;
using Gymopedia.Inputs;
using Gymopedia.Core.CoachWorkDays;
using Gymopedia.Core.Sessions;
using MediatR;

namespace Gymopedia.Controllers
{
    [Route("/coahworkday")]
    [ApiController]
    public class CoachWorkDayController : Controller
    {
        private readonly IMediator _mediator;

        public CoachWorkDayController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/createCoachWorkDay")]
        public async Task<CoachWorkDayDto> CreateCoachWorkDay(CreateCoachWorkDayInput input, CancellationToken cancellationToken)
        {
            var request = new CreateCoachWorkDay.Request(input.From, input.Until,input.MaxClient);
            var createCoachWorkDayResponse = await _mediator.Send(request, cancellationToken);

            for (DateTime t = request.From; t < request.Until; t.AddHours(1))
            {
                var reqiestSession = new CreateSession.Request(
                    t,
                    t.AddHours(1),
                    request.MaxClient,
                    createCoachWorkDayResponse.CoachWorkDays.Id);
                var createSessionResponse = await _mediator.Send(reqiestSession, cancellationToken);

                createCoachWorkDayResponse.CoachWorkDays.sessions.Add(
                    new Domain.Models.Session(createSessionResponse.Session.From,
                    createSessionResponse.Session.Until,
                    createSessionResponse.Session.MaxClient,
                    createSessionResponse.Session.CoachWorkDayId));
            }

            return new CoachWorkDayDto
            {
                Date = createCoachWorkDayResponse.CoachWorkDays.Date
            };
        }
    }
}
