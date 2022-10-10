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
    public class ClientToSessionController
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
    }
}
