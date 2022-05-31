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
    }
}
