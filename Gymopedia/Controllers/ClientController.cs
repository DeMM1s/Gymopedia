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

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("/createClient")]
        public async Task<ClientDto> CreateClient(CreateClientInput input, CancellationToken cancellationToken)
        {
            var request = new CreateClient.Request(input.Name, input.CoachId);
            var createClientResponse = await _mediator.Send(request, cancellationToken);

            return new ClientDto
            {
                Name = createClientResponse.Client.Name,
                CoachIds = createClientResponse.Client.CoachIds
            };
        }
        [HttpGet]
        [Route("/getClient")]
        public async Task<IActionResult> Get(int clientId, CancellationToken cancellationToken)
        {
            var request = new GetClient.Request(clientId);

            var getClientResponse = await _mediator.Send(request, cancellationToken);

            return Ok(getClientResponse);
        }
        [HttpPut]
        [Route("/editClient")]
        public async Task<IActionResult> Edit(int clientId, CancellationToken cancellationToken)
        {
            var request = new EditClient.Request(clientId);

            var getClientResponse = await _mediator.Send(request, cancellationToken);

            return Ok(getClientResponse);
        }
        [HttpDelete]
        [Route("/deleteClient")]
        public async Task<IActionResult> Delete (int clientId, CancellationToken cancellationToken)
        {
            var request = new DeleteClient.Request(clientId);

            var getClientResponse = await _mediator.Send(request, cancellationToken);

            return Ok(getClientResponse);
        }
    }
}
