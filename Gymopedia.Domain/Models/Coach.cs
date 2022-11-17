namespace Gymopedia.Domain.Models
{
    public class Coach
    {
        public int Id { get; init; }

        public long ChatId { get; init; }

        public string? Name { get; set; }
        public string FullName { get; set; }
        public string? Description { get; set; }

        public Coach() { }

        public Coach(string name, string fullName, long chatId)
        {
            Name = name;
            FullName = fullName;
            ChatId = chatId;
        }
    }
}
