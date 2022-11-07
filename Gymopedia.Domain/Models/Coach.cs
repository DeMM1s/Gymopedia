namespace Gymopedia.Domain.Models
{
    public class Coach
    {
        public int Id { get; init; }

        public long ChatId { get; init; }

        public string Name { get; set; }

        public Coach() { }

        public Coach(string name, long chatId)
        {
            Name = name;
            ChatId = chatId;
        }
    }
}
