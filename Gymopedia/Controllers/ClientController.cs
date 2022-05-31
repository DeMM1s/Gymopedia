using Microsoft.AspNetCore.Mvc;
using Gymopedia.Core.Models;
using Gymopedia.Core.Clients;
using Gymopedia.Inputs;
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

        [HttpPost]
        [Route("/create")]
        public async Task<ClientDto> CreateClient(CreateClientInput input, CancellationToken cancellationToken)
        {
            var request = new CreateClient.Request(input.Name,input.CoachId);
            var createClientResponse = await _mediator.Send(request, cancellationToken);

            return new ClientDto
            {
                Name = createClientResponse.Client.Name,
                CoachIds = createClientResponse.Client.CoachIds
            };
        }
    }
}
