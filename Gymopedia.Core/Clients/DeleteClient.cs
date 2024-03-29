﻿using Gymopedia.Domain.DtoModels;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.Clients
{
    public class DeleteClient
    {
        public record Request(int ID) : IRequest<Response>;
        public record Response(ClientDto? Client, string? Error = null);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IClientRepository _clientRepository;
            public Handler(IClientRepository clientRepository)
            {
                _clientRepository = clientRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var client = await _clientRepository.Delete(request.ID, cancellationToken);
                if (client == null)
                {
                    return new Response(null, Constants.ErrorMessage.Client.ClientNotFoundError);
                }
                return new Response(new ClientDto
                {
                    Name = client.Name,
                    Id = client.Id,
                });
            }
        }
    }
}