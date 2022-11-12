using Microsoft.AspNetCore.Mvc;
using Gymopedia.Core.Models;
using Gymopedia.Inputs;
using Gymopedia.Domain.Models;
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

        [HttpGet]
        [Route("/getSession")]
        public async Task<IActionResult> Get(int sessionId, CancellationToken cancellationToken)
        {
            var request = new GetSession.Request(sessionId);

            var getSessionResponse = await _mediator.Send(request, cancellationToken);

            return Ok(getSessionResponse);
        }
        [HttpPut]
        [Route("/editSession")]
        public async Task<IActionResult> Edit(Session session, CancellationToken cancellationToken)
        {
            var request = new EditSession.Request(session);

            var editSessionResponse = await _mediator.Send(request, cancellationToken);

            return Ok(editSessionResponse);
        }
        [HttpDelete]
        [Route("/deleteSession")]
        public async Task<IActionResult> Delete(int sessionId, CancellationToken cancellationToken)
        {
            var request = new DeleteSession.Request(sessionId);

            var deleteSessionResponse = await _mediator.Send(request, cancellationToken);

            return Ok(deleteSessionResponse);
        }
    }
}
