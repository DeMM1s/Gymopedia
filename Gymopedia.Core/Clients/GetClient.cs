using Gymopedia.Core.Models;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.Clients
{
    public class GetClient
    {
        public record Request(int ID) : IRequest<Response>;
        public record Response(ClientDto? Client, string? Error = null);

        public class Heandler : IRequestHandler<Request, Response>
        {
            private readonly IClientRepository _clientRepository;
            public Heandler(IClientRepository clientRepository)
            {
                _clientRepository = clientRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var client = await _clientRepository.Get(request.ID, cancellationToken);
                if (client == null)
                {
                    return new Response(null, Constants.ErrorMessage.Client.ClientNotFoundError);
                }
                return new Response(new ClientDto
                {
                    Name = client.Name,
                    Id = client.Id,
                    CoachIds = client.CoachIds,
                    TrainingSessions = client.TrainingSessions,
                });
            }
        }
    }
}
