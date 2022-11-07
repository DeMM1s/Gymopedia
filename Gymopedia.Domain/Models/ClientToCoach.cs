
namespace Gymopedia.Domain.Models
{
    public class ClientToCoach
    {
        public int Id { get; set; }
        public long CoachId { get; set; }
        public long ClientId { get; set; }
    }
}
