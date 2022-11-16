using Deployf.Botf;
using Telegram.Bot;

namespace Gymopedia.Core.Telegram
{
    public class Sendler
    {
        readonly MessageBuilder Message;
        readonly MessageSender Sender;

        public Sendler(ITelegramBotClient client)
        {
            Message = new MessageBuilder();
            Sender = new MessageSender(client);
        }

        public async Task Exec(string message, long chatId)
        {
            Message.Push(message);
            Message.SetChatId(chatId);
            await Sender.Send(Message);
        }
    }
}
