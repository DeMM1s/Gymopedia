using Gymopedia.Domain.Models;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Domain.Repositories
{
    public interface IClientToCoachRepository : IRepository
    { 
        void Add(ClientToCoach clientToCoach);
        Task<ClientToCoach?> Get(int clientId, CancellationToken cancellationToken);
        Task<ClientToCoach?> Delete(int clientId, int coachId, CancellationToken cancellationToken);
    }
}
