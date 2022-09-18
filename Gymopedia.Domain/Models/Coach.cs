namespace Gymopedia.Domain.Models
{
    public class Coach
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public ICollection<Client> Clients { get; init; } = new List<Client>();

        public Calendar? Calendar { get; init; }

        public Coach() { }

        public Coach(string name)
        {
            Name = name;
        }
    }
}
