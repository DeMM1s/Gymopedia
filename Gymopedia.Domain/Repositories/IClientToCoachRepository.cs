using Gymopedia.Domain.Models;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Domain.Repositories
{
    public interface IClientToCoachRepository : IRepository
    { 
        void Add(ClientToCoach clientToCoach);
        Task<ClientToCoach?> Get(long clientId, long coachId, CancellationToken cancellationToken);
        Task<List<ClientToCoach>> GetAll(long clientId, CancellationToken cancellationToken);
        Task<ClientToCoach?> Delete(long clientId, long coachId, CancellationToken cancellationToken);
    }
}
