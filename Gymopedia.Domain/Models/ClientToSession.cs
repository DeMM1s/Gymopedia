
namespace Gymopedia.Domain.Models
{
    public class ClientToSession
    {
        public int Id { get; set; }
        public long SessionId { get; set; }
        public long ClientId  { get; set; }
    }
}
