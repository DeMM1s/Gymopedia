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


            return new CoachWorkDayDto
            {
                Date = createCoachWorkDayResponse.CoachWorkDays.Date
            };
        }
    }
}
