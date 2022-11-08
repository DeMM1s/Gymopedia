using Microsoft.AspNetCore.Mvc;
using Gymopedia.Core.Models;
using Gymopedia.Domain.Models;
using Gymopedia.Core.Clients;
using Gymopedia.Core.Coaches;
using Gymopedia.Core.Sessions;
using Gymopedia.Core.ClientToCoachs;
using Gymopedia.Core.ClientToSessions;
using Gymopedia.Inputs;
using MediatR;
using Deployf.Botf;

namespace Gymopedia.Controllers
{
    //[Route("/client")]
    public class ClientController : BotController
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Action("/registrationClient")]
        public async Task registrationClient()
        {
            var chatId = Context.UserId();
            var client = await Get(chatId);

            if (client == null)
            {
                var Name = Context.GetUsername();
                if (Name == null)
                {

                }
                client = await CreateClient(Name, chatId);
                PushL("Вы успешно зарегестрированы");
            }
            else
            {
                PushL("Мы вас помним");
            }
            PushL($"Здравсвуйте, {client.Name}");
            await Send();
            ClientMenu();
        }

        [Action]
        public async Task ClientMenu()
        {
            PushL("Меню");
            RowButton("Посмотреть информацию обо мне",Q(AboutMe));
            RowButton("Найти тренера", Q(FindCoach));
            RowButton("Мои тренера", Q(ListOfCoaches));
            RowButton("Мои записи", Q(ListOfSessions));
            RowButton("Ближайшая тренировка", Q(NearestSession));
            Send();
        }

        [Action]
        public async Task AboutMe()
        {
            var chatId = Context.UserId();
            var client = await Get(chatId);

            PushL($"Ваше имя: {client.Name}");
            RowButton("Вернуться в меню", Q(ClientMenu));
        }
        [Action]
        public async Task ListOfCoaches()
        {
            var chatId = Context.UserId();
            var request = new GetAllClientToCoach.Request(chatId);
            var ListResponse = await _mediator.Send(request);
            var list = ListResponse.ClientToCoachList;
            PushL("Вы подписаны на:");
            foreach (var item in list)
            {
                var requestCoach = new GetCoach.Request(item.CoachId);
                var getCoachResponse = await _mediator.Send(requestCoach);
                var coach = getCoachResponse.Coach;
                RowButton(coach.Name, Q(CoachMenu,coach));
            }
            RowButton("Вернуться в меню", Q(ClientMenu));
        }

        [Action]
        public async Task CoachMenu(Coach coach)
        {
            PushL($"Тренер: {coach.Name}");
            RowButton("Посмореть информацию", Q(ClientMenu));
            RowButton("Сессии", Q(ClientMenu));
            RowButton("Отписаться", Q(Unsubscribe, coach));
            RowButton("Вернуться в меню", Q(ClientMenu));
        }

        [Action]
        public async Task ListOfSessions()
        {
            
        }
        [Action]
        public async Task NearestSession()
        {

        }


        [Action("/GetFromName")]
        public async Task FindCoach()
        {
            PushL("Введите имя теренера");
            Fill_Comment(await GetAState<FillState>());
            await Send();
        }

        struct FillState
        {
            public string? Comment { get; set; }
            public bool IsSet => !string.IsNullOrEmpty(Comment);
        }


        [Action]
        void Fill_Comment([State] FillState state)
        {
            State(new SetCommentState());
            RowButton("Найти", Q(Name));
        }



        [Action]
        async Task Name()
        {
            var fillState = await GetAState<FillState>();
            string Name = fillState.Comment;
            var coach = await GetFromName(Name);

            if(coach!= null) 
            {
                PushL(coach.Name);
                PushL(coach.Id.ToString());
                PushL(coach.ChatId.ToString());

                RowButton("подписаться", Q(Subscribe, coach) );
            }
            else
            {
                PushL("Тренер не найден");
                RowButton("Попробовать снова", Q(FindCoach));
                RowButton("Вернуться в меню", Q(ClientMenu));
            }
        }

        [Action]
        public async ValueTask Unsubscribe(Coach coach)
        {
            var chatId = Context.UserId();
            await DeleteClientToCoach(chatId, coach.ChatId);
            PushL($"Вы отписались от {coach.Name}");
            RowButton("Отмена", Q(Subscribe, coach));
            RowButton("Вернуться в меню", Q(ClientMenu));
            await Send();
        }

        [Action]
        public async ValueTask Subscribe(Coach coach)
        {
            var chatId = Context.UserId();
            var subscribeToCoach = await SubscribeToCoach(chatId, coach.ChatId);
            PushL($"Вы подписались на {coach.Name}");
            PushL(subscribeToCoach.Id.ToString());
            RowButton("Вернуться в меню", Q(ClientMenu));
            await Send();
        }

        [State]
        async ValueTask Fill_StateComment(SetCommentState state)
        {
            var fillState = await GetAState<FillState>();
            fillState = fillState with { Comment = Context.GetSafeTextPayload() };
            await AState(fillState);
        }
        record SetCommentState;

        #region clientToCoach
        [HttpPost]
        [Route("/subscribeToCoach")]
        public async Task<ClientToCoach> SubscribeToCoach(long clientId, long coachId)
        {
            var request = new SubscribeToCoach.Request(clientId, coachId);
            var subscribeToCoachResponse = await _mediator.Send(request);

            return subscribeToCoachResponse.ClientToCoach;
        }
        [HttpGet]
        [Route("/getClientToCoach")]
        public async Task Get(long clientId, long coachId, CancellationToken cancellationToken)
        {
            var request = new GetClientToCoach.Request(clientId, coachId);

            var getClientToCoachResponse = await _mediator.Send(request, cancellationToken);

        }
        [HttpDelete]
        [Route("/deleteClientToCoach")]
        public async Task DeleteClientToCoach(long clientId, long coachId)
        {
            var request = new DeleteClientToCoach.Request(clientId, coachId);

            var deleteClientToCoachResponse = await _mediator.Send(request);

        }
        #endregion

        #region rest

        [Action]
        public async Task<Coach> GetFromName(string Name)
        {
            var request = new GetCoachFromName.Request(Name);
            var getCoachResponse = await _mediator.Send(request);
            return getCoachResponse.Coach;
        }

        [Action("/createClient")]
        public async Task<Client> CreateClient(string Name, long chatId)
        {
            var request = new CreateClient.Request(Name, chatId);
            var createClientResponse = await _mediator.Send(request);
            return createClientResponse.Client;
        }
        [Action("/getClient")]
        public async Task<Client> Get(long chatId)
        {
            var request = new GetClient.Request(chatId);

            var getClientResponse = await _mediator.Send(request);
            return getClientResponse.Client;

        }

        [Action("/editClient")]
        public async Task Edit(int clientId, string name , CancellationToken cancellationToken)
        {
            var request = new EditClient.Request(new Domain.Models.Client { Id = clientId, Name = name});

            var getClientResponse = await _mediator.Send(request, cancellationToken);

        }
        [Action("/deleteClient")]
        public async Task Delete (int clientId, CancellationToken cancellationToken)
        {
            var request = new DeleteClient.Request(clientId);

            var getClientResponse = await _mediator.Send(request, cancellationToken);

        }
        #endregion
    }
}
