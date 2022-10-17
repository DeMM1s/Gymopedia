using Microsoft.AspNetCore.Mvc;
using Gymopedia.Core.Models;
using Gymopedia.Core.Clients;
using Gymopedia.Core.ClientToCoachs;
using Gymopedia.Core.ClientToSessions;
using Gymopedia.Inputs;
using MediatR;

namespace Gymopedia.Controllers
{
    [Route("/ClientToSession")]
    [ApiController]
    public class ClientToSessionController : Controller
    {
        private readonly IMediator _mediator;

        public ClientToSessionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("/subscribeToSession")]
        public async Task<ClientToSessionDTO> SubscribeToSession(int clientId, int sessionId, CancellationToken cancellationToken)
        {
            var request = new SubscribeToSession.Request(clientId, sessionId);
            var subscribeToSessionResponse = await _mediator.Send(request, cancellationToken);
            return new ClientToSessionDTO { ClientId = clientId, SessionId = sessionId };
        }
        [HttpGet]
        [Route("/getClientToSession")]
        public async Task<IActionResult> Get(int clientId, int sessionId, CancellationToken cancellationToken)
        {
            var request = new GetClientToSession.Request(clientId, sessionId);

            var getClientToSessionResponse = await _mediator.Send(request, cancellationToken);

            return Ok(getClientToSessionResponse);
        }
        [HttpDelete]
        [Route("/deleteClientToSession")]
        public async Task<IActionResult> Delete(int clientId, int sessionId, CancellationToken cancellationToken)
        {
            var request = new DeleteClientToSession.Request(clientId, sessionId);

            var deleteClientToSessionResponse = await _mediator.Send(request, cancellationToken);

            return Ok(deleteClientToSessionResponse);
        }
    }
}
