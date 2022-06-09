using Microsoft.AspNetCore.Mvc;
using Gymopedia.Core.Models;
using Gymopedia.Inputs;
using Gymopedia.Core.Sessions;
using MediatR;

namespace Gymopedia.Controllers
{
    [Route("/session")]
    [ApiController]
    public class SessionController : Controller
    {
        private readonly IMediator _mediator;

        public SessionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/createSession")]
        public async Task<SessionDto> CreateSession(CreateSessionInput input, CancellationToken cancellationToken)
        {
            var request = new CreateSession.Request(input.From, input.Until, input.MaxClient, input.CoachWorkDayId);
            var createSessionResponse = await _mediator.Send(request, cancellationToken);

            return new SessionDto
            {
                From = createSessionResponse.Session.From,
                Until = createSessionResponse.Session.Until,
                MaxClient = createSessionResponse.Session.MaxClient,
                ClientIds = createSessionResponse.Session.ClientIds,
                CoachWorkDayId = createSessionResponse.Session.CoachWorkDayId,
            };
        }
    }
}
