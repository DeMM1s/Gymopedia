﻿using Microsoft.EntityFrameworkCore;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;

namespace Gymopedia.Data.Repository
{
    public class ClientToSessionRepository: MasterRepository, IClientToSessionRepository
    {
        public ClientToSessionRepository(LocalDbContext context) : base(context)
        {

        }

        public void Add(ClientToSession ClientToSession)
        {
            Context.ClientToSession.Add(ClientToSession);
        }

        public async Task<ClientToSession?> Get(int clientId,int sessionId, CancellationToken cancellationToken)
        {
            return await Context.ClientToSession.FirstOrDefaultAsync(o => o.Id == clientId, cancellationToken);
        }

        public async Task<ClientToSession?> Delete(int clientId, int sessionId, CancellationToken cancellationToken)
        {
            ClientToSession? ClientToSession = await Context.ClientToSession.SingleOrDefaultAsync(o => o.Id == clientId, cancellationToken);
            if (ClientToSession == null)
            {
                return null;
            }
            Context.ClientToSession.Remove(ClientToSession);
            Context.SaveChanges();
            return ClientToSession;
        }
    }
}