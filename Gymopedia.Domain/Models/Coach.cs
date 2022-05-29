namespace Gymopedia.Domain.Models
{
    public class Coach
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public ICollection<Client> Clients { get; init; } = new List<Client>();

        public Calendar? Calendar { get; init; }
    }
}
