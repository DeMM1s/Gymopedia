namespace Gymopedia.Domain.Models
{
    public class Client
    {
        public int Id { get; init; }

        public long ChatId { get; init; }
        public string Name { get; set; }

        public Client() { }
        public Client(string name, long chatId)
        {
            Name = name;
            ChatId = chatId;
        }
    }
}