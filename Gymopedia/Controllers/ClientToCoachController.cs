using Microsoft.AspNetCore.Mvc;
using Gymopedia.Core.Models;
using Gymopedia.Core.Clients;
using Gymopedia.Core.Coaches;
using Gymopedia.Core.Sessions;
using Gymopedia.Core.ClientToCoachs;
using Gymopedia.Core.ClientToSessions;
using Gymopedia.Inputs;
using MediatR;

namespace Gymopedia.Controllers
{
    [Route("/ClientToCoach")]
    [ApiController]
    public class ClientToCoachController : Controller
    {
        private readonly IMediator _mediator;

        public ClientToCoachController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("/subscribeToCoach")]
        public async Task<ClientToCoachDTO> SubscribeToCoach(int clientId, int coachId, CancellationToken cancellationToken)
        {
            var request = new SubscribeToCoach.Request(clientId, coachId);
            var subscribeToCoachResponse = await _mediator.Send(request, cancellationToken);

            return new ClientToCoachDTO { ClientId = clientId, CoachId = coachId };
        }
        [HttpGet]
        [Route("/getClientToCoach")]
        public async Task<IActionResult> Get(int clientId, int coachId, CancellationToken cancellationToken)
        {
            var request = new GetClientToCoach.Request(clientId, coachId);

            var getClientToCoachResponse = await _mediator.Send(request, cancellationToken);

            return Ok(getClientToCoachResponse);
        }
        [HttpDelete]
        [Route("/deleteClientToCoach")]
        public async Task<IActionResult> Delete(int clientId, int coachId, CancellationToken cancellationToken)
        {
            var request = new DeleteClientToCoach.Request(clientId, coachId);

            var deleteClientToCoachResponse = await _mediator.Send(request, cancellationToken);

            return Ok(deleteClientToCoachResponse);
        }
    }
}
