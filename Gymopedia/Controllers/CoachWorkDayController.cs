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
                Date = createCoachWorkDayResponse.CoachWorkDays.Date,
                Sessions = createCoachWorkDayResponse.CoachWorkDays.Sessions
            };
        }
        [HttpGet]
        [Route("/getCoachWorkDay")]
        public async Task<IActionResult> Get(int coachWorkDayId, CancellationToken cancellationToken)
        {
            var request = new GetCoachWorkDay.Request(coachWorkDayId);

            var getCoachWorkDayResponse = await _mediator.Send(request, cancellationToken);

            return Ok(getCoachWorkDayResponse);
        }
        [HttpPut]
        [Route("/editCoachWorkDay")]
        public async Task<IActionResult> Edit(int coachWorkDayId, CancellationToken cancellationToken)
        {
            var request = new EditCoachWorkDay.Request(coachWorkDayId);

            var editCoachWorkDayResponse = await _mediator.Send(request, cancellationToken);

            return Ok(editCoachWorkDayResponse);
        }
        [HttpDelete]
        [Route("/deleteCoachWorkDay")]
        public async Task<IActionResult> Delete(int coachWorkDayId, CancellationToken cancellationToken)
        {
            var request = new DeleteCoachWorkDay.Request(coachWorkDayId);

            var deleteCoachWorkDayResponse = await _mediator.Send(request, cancellationToken);

            return Ok(deleteCoachWorkDayResponse);
        }
    }
}
