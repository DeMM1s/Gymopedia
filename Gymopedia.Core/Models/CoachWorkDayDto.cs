using Gymopedia.Domain.Models;

namespace Gymopedia.Core.Models
{
    public class CoachWorkDayDto
    {
        public int Id { get; init; }
        public DateOnly Date { get; init; }
        public ICollection<Session> sessions { get; init; } = new List<Session>();
    }
}
