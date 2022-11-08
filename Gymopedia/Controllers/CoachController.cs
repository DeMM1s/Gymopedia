using Microsoft.AspNetCore.Mvc;
using Gymopedia.Core.Models;
using Gymopedia.Inputs;
using Gymopedia.Core.Coaches;
using MediatR;
using Deployf.Botf;
using Gymopedia.Domain.Models;

namespace Gymopedia.Controllers
{
    //[Route("/coach")]
    public class CoachController : BotController
    {
        private readonly IMediator _mediator;

        public CoachController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Action("/registrationCoach")]
        public async Task registrationCoach()
        {
            var chatId = Context.UserId();
            var coach = await Get(chatId);
            if (coach == null)
            {
                var Name = Context.GetUsername();
                coach = await Create (Name, chatId);
                PushL("Вы успешно зарегестрированы");
            }
            else
            {
                PushL("Мы вас помним");
            }
            PushL($"Здравсвуйте, {coach.Name}");
            await Send();
            CoachMenu();
        }

        public async Task CoachMenu()
        {
            PushL("Меню");
            RowButton("Посмотреть информацию обо мне", Q(AboutMe));
            RowButton("Мои сессии",Q(MySessions));
            RowButton("Ближайший клиент",Q(NearestClient));
            Send();
        }

        [Action]
        public async Task AboutMe()
        {
            var chatId = Context.UserId();
            var coach = await Get(chatId);

            PushL($"Ваше имя: {coach.Name}");
            RowButton("Вернуться в меню", Q(CoachMenu));
        }
        [Action]
        public async Task MySessions()
        {
            PushL("Мои сессии");
            RowButton("Создать", Q(SessionController));
            RowButton("Посмотреть список", Q(ListOfSessions));
            RowButton("Вернуться в меню", Q(CoachMenu));
        }
        [Action]
        public async Task ListOfSessions()
        {

        }

        [Action]
        public async Task SessionController()
        {

        }

        [Action]
        public async Task NearestClient()
        {

        }


        #region REST
        [Action]
        public async Task<Coach> Create(string Name, long chatId)
        {
            var request = new CreateCoach.Request(Name, chatId);
            var createCoachResponse = await _mediator.Send(request);
            return createCoachResponse.Coach;
        }

        [Action]
        public async Task<Coach> Get(long chatId)
        { 
            var request = new GetCoach.Request(chatId);
            var getCoachResponse = await _mediator.Send(request);
            return getCoachResponse.Coach;
        }


        [Action]
        public async Task Edit()
        {
            var request = new EditCoach.Request(new Domain.Models.Coach { Id = 0, Name = ""});

            var editCoachResponse = await _mediator.Send(request);

        }

        [Action]
        public async Task Delete()
        {
            var request = new DeleteCoach.Request(0);

            var deleteCoachResponse = await _mediator.Send(request);

        }
        #endregion
    }
}
