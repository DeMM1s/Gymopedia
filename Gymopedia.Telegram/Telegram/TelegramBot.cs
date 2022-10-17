using Telegram.Bot;
using Microsoft.Extensions.Configuration;
namespace Gymopedia.Telegram.Telegram
{
    public class TelegramBot
    {
        private readonly IConfiguration _configuration;
        private TelegramBotClient telegramBot;

        public TelegramBot(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<TelegramBotClient> GetBot()
        {
            if (telegramBot != null) { return telegramBot; }

            telegramBot = new TelegramBotClient("5493236640:AAHJ9TKD1BQ2rbYMfQtnaxiwVHpsE9GYlrE");
            var hook = "https://95fd-37-145-42-200.eu.ngrok.io/api/message";

            await telegramBot.SetWebhookAsync(hook);
            return telegramBot;
        }
    }
}
