namespace Gymopedia.Domain.Models
{
    public class Client
    {
        public int Id { get; init; }
        public string Name { get; set; }

        public ICollection<Coach> CoachIds { get; init; } = new List<Coach>();
        public ICollection<Session> TrainingSessions { get; init; } = new List<Session>();

        public Client() { }
        public Client(string name)
        {
            Name = name;
        }
    }
}