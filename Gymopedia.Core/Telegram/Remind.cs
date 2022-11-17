using Deployf.Botf;
using Telegram.Bot;
using Gymopedia.Domain.DtoModels;
using Gymopedia.Domain.Repositories;
using MediatR;

namespace Gymopedia.Core.Telegram
{
    public class Remind
    {
        public readonly IMediator _mediator;
        public int Id { get; set; } 
        public ITelegramBotClient Client { get; set; } 
        public DateTime Time { get { return Session.From.AddHours(-2).ToLocalTime(); }}  
        public SessionDto Session { get; set; }
        public Remind(ITelegramBotClient client, SessionDto session, IMediator mediator)
        {
            Client = client;
            Session = session;
            _mediator = mediator;       
        }
    }
}
