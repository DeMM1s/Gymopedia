using Gymopedia.Core.Models;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.ClientToCoachs
{
    public class GetAllClientToCoach
    {
        public record Request(long clientId) : IRequest<Response>;
        public record Response(List<ClientToCoach> ClientToCoachList, string? Error = null);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IClientToCoachRepository _clientToCoachRepository;
            public Handler(IClientToCoachRepository clientToCoachRepository)
            {
                _clientToCoachRepository = clientToCoachRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var List = await _clientToCoachRepository.GetAll(request.clientId, cancellationToken);
                return new Response(List);
            }
        }
    }
}
