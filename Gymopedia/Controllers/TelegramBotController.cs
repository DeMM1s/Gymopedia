using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using MediatR;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Gymopedia.Telegram.Telegram;
using Deployf.Botf;
using System.Threading;

namespace Gymopedia.Controllers
{
    public class TelegramBotController : BotController
    {
        [Action("/start")]
        public async Task Start()
        {
            PushL("Здравствуйте.");
            PushL("Это бот для записи к персональному тренеру.");
            await Send();
            ButtonStart();
        }

        [Action]
        public void ButtonStart()
        {
            PushL("Выберете действие");
            RowButton("Показать описание", Q(ShowDescription));
            RowButton("Войти как тренер", Q<CoachController>(c => c.registrationCoach));
            RowButton("Войти как клиент", Q<ClientController>(c => c.registrationClient));
        }
        [Action]
        public void ShowDescription()
        {
            PushL("Этот бот позволяет записаться к вашему персональному тренеру. Вам нужно знать его @ИмяПользователя в Telegram");
            RowButton("Вернуться", Q(ButtonStart));
        }

        public async void ScheduleAction(Action action, DateTime ExecutionTime)
        {
            await Task.Delay((int)ExecutionTime.Subtract(DateTime.Now).TotalMilliseconds);
            action();
        }

    }
}
