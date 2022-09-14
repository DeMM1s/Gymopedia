namespace Gymopedia.Domain.Models
{
    public class Client
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public ICollection<CoachIdsList> CoachIds { get; init; } = new List<CoachIdsList>();
        public ICollection<Session> TrainingSessions { get; init; } = new List<Session>();

        public Client() { }
        public Client(string name, int coachId)
        {
            Name = name;
            CoachIds.Add(new CoachIdsList(0, coachId));
        }
    }
}