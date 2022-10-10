
namespace Gymopedia.Domain.Models
{
    public class ClientToSession
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int ClientId  { get; set; }
    }
}
