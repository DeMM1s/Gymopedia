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
            }
            PushL(coach.Name);
            PushL(coach.Id.ToString());
            PushL(coach.ChatId.ToString());

            //RowButton("Найти тренера", Q(FindCoach));
        }

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
    }
}
