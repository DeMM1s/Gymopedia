using Gymopedia.Core.Models;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.Clients
{
    public class GetClient
    {
        public record Request(long ChatId) : IRequest<Response>;
        public record Response(Client? Client, string? Error = null);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IClientRepository _clientRepository;
            public Handler(IClientRepository clientRepository)
            {
                _clientRepository = clientRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var client = await _clientRepository.Get(request.ChatId, cancellationToken);
                if (client == null)
                {
                    return new Response(null, Constants.ErrorMessage.Client.ClientNotFoundError);
                }
                return new Response(client);
            }
        }
    }
}