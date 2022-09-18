using Microsoft.AspNetCore.Mvc;
using Gymopedia.Core.Models;
using Gymopedia.Inputs;
using Gymopedia.Core.Coaches;
using MediatR;

namespace Gymopedia.Controllers
{
    [Route("/coach")]
    [ApiController]
    public class CoachController : Controller
    {
        private readonly IMediator _mediator;

        public CoachController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/createCoach")]
        public async Task<CoachDto> CreateCoach(CreateCoachInput input, CancellationToken cancellationToken)
        {
            var request = new CreateCoach.Request(input.Name);
            var createCoachResponse = await _mediator.Send(request, cancellationToken);

            return new CoachDto
            {
                Name = createCoachResponse.Coach.Name
            };
        }
        [HttpGet]
        [Route("/getCoach")]
        public async Task<IActionResult> Get(int coachId, CancellationToken cancellationToken)
        {
            var request = new GetCoach.Request(coachId);

            var getCoachResponse = await _mediator.Send(request, cancellationToken);

            return Ok(getCoachResponse);
        }
        [HttpPut]
        [Route("/editCoach")]
        public async Task<IActionResult> Edit(int coachId, string name, CancellationToken cancellationToken)
        {
            var request = new EditCoach.Request(new Domain.Models.Coach { Id = coachId, Name = name});

            var editCoachResponse = await _mediator.Send(request, cancellationToken);

            return Ok(editCoachResponse);
        }
        [HttpDelete]
        [Route("/deleteCoach")]
        public async Task<IActionResult> Delete(int coachId, CancellationToken cancellationToken)
        {
            var request = new DeleteCoach.Request(coachId);

            var deleteCoachResponse = await _mediator.Send(request, cancellationToken);

            return Ok(deleteCoachResponse);
        }
    }
}
