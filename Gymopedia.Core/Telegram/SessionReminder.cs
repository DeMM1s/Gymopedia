using Deployf.Botf;
using Telegram.Bot;
using Gymopedia.Domain.DtoModels;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using MediatR;
using Gymopedia.Core.ClientToSessions;
using System.Text.Json.Serialization;

namespace Gymopedia.Core.Telegram
{
    public class SessionReminder
    {
        readonly MessageBuilder Message;
        readonly MessageSender Sender;
        private IMediator _mediator;
        private readonly Remind remind;


        public SessionReminder()
        {

        }
        public SessionReminder(MessageSender sender)
        {
            Message = new MessageBuilder();
            Sender = sender;
        }

        public async Task SendRemind(Remind remind)
        {
            _mediator = remind._mediator;
            var request = new GetAllClientsBySession.Request(remind.Session.Id);
            var ListResponse = await _mediator.Send(request);
            var list = ListResponse.ClientToSessionList;
            if (list.Any())
            {
                await Exec($"У вас клиент в {remind.Session.From.ToLocalTime()}", remind.Session.CoachId);
                foreach (var item in list)
                {
                    Exec($"У вас сессия в {remind.Session.From.ToLocalTime()}", item.ClientId);
                }
            }
        }

        public async Task Exec(string message, long chatId)
        {
            Message.Push(message);
            Message.SetChatId(chatId);
            await Sender.Send(Message);
        }

    }
}
