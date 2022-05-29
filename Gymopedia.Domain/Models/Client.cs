namespace Gymopedia.Domain.Models
{
    public class Client
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public ICollection<int> CoachIds { get; init; } = new List<int>();
        public ICollection<Session> TrainingSessions { get; init; } = new List<Session>();
        public Client(string name)
        {
            Name = name;
        }
    }
}