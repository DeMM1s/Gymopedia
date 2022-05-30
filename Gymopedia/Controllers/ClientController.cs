using Microsoft.AspNetCore.Mvc;
using Gymopedia.Core.Models;
using Gymopedia.Core.Clients;
using MediatR;

namespace Gymopedia.Controllers
{
    [Route("/client")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly IMediator _mediator;

        public ClientController (IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/create")]
        public async Task<ClientDto> Create(CreateClient.Request request, CancellationToken cancellationToken)
        {
            var createClientResponse = await _mediator.Send(request, cancellationToken);

            return new ClientDto
            {
                Name = createClientResponse.Client.Name,
                CoachIds = createClientResponse.Client.CoachIds
            };
        }
    }
}
