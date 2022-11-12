namespace Gymopedia.Domain.Models
{
    public class MyCalendar
    {
        public int Id { get; init; }
        public int OwnerCoachId { get; init; }
        public ICollection<CoachWorkDay> CoachWorkDays { get; init; } = new List<CoachWorkDay>();
        public MyCalendar()        {        }
    }
}
