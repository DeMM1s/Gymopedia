using Gymopedia.Domain.Models;

namespace Gymopedia.Domain.DtoModels
{
    public class CoachWorkDayDto
    {
        public int Id { get; init; }
        public DateTime Date { get; init; }
        public ICollection<Session> Sessions { get; init; } = new List<Session>();
    }
}
