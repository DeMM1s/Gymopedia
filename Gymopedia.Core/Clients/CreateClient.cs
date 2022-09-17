using Gymopedia.Core.Models;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using MediatR;

namespace Gymopedia.Core.Clients
{
    public class CreateClient
    {
        public record Request(string Name, int CoachId) : IRequest<Response>;

        public record Response(ClientDto Client);
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IClientRepository _clientRepository;
            public Handler(IClientRepository clientRepository)
            {
                _clientRepository = clientRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var client = new Client(
                    request.Name);

                //client.CoachIds.Add(request.CoachId);

                _clientRepository.Add(client);
                await _clientRepository.Commit(cancellationToken);

                return new Response(new ClientDto
                {
                    Name = request.Name,
                    CoachIds = request.CoachId
                });
            }
        }
    }
}
