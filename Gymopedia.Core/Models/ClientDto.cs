using Gymopedia.Domain.Models;

namespace Gymopedia.Core.Models
{
    public class ClientDto
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public int CoachIds { get; init; }

        //public ICollection<int> CoachIds { get; init; } = new List<int>();
        public ICollection<Session> TrainingSessions { get; init; } = new List<Session>();
    }
}
