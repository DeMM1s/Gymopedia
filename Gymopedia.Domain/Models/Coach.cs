namespace Gymopedia.Domain.Models
{
    public class Coach
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public Coach() { }

        public Coach(string name)
        {
            Name = name;
        }
    }
}
