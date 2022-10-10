
namespace Gymopedia.Domain.Models
{
    public class ClientToCoach
    {
        public int Id { get; set; }
        public int CoachId { get; set; }
        public int ClientId { get; set; }
    }
}
