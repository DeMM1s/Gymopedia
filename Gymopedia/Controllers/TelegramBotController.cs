using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using MediatR;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Gymopedia.Telegram.Telegram;
using Deployf.Botf;

namespace Gymopedia.Controllers
{
    public class TelegramBotController : BotController
    {
        [Action("/start")]
        public void Start()
        {
            PushL("привет димон");
        }
    }
}
