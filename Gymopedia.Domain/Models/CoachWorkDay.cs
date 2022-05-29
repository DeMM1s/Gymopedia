namespace Gymopedia.Domain.Models
{
    public class CoachWorkDay
    {
        public int Id { get; init; }
        public DateOnly Date { get; init; }
        public ICollection<Session> sessions { get; init; } = new List<Session>();
        public CoachWorkDay(DateOnly date)
        {
            Date = date;
        }
    }
}
