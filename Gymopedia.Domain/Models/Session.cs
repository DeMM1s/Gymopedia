namespace Gymopedia.Domain.Models
{
    public class Session
    {
        public int Id { get; init; }
        private readonly DateTime _From;
        private readonly DateTime _Until;
        private int _MaxClient;
        public int CoachWorkDayId { get; init; }
        public ICollection<int> ClientIds { get; init; } = new List<int>();
        public void SetMaxClient(int MaxClient)
        {
            _MaxClient = MaxClient;
        }
        public Session(DateTime from, DateTime until, int maxClient, int coachWorkDayId)
        {
            _From = from;
            _Until = until;
            _MaxClient = maxClient;
            CoachWorkDayId = coachWorkDayId;
        }
    }
}