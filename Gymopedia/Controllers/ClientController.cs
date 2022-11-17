using Microsoft.AspNetCore.Mvc;
using Gymopedia.Domain.Models;
using Gymopedia.Domain.DtoModels;
using Gymopedia.Core.Clients;
using Gymopedia.Core.Coaches;
using Gymopedia.Core.Sessions;
using Gymopedia.Core.ClientToCoachs;
using Gymopedia.Core.ClientToSessions;
using Gymopedia.Core.Telegram;
using MediatR;
using Deployf.Botf;
using Hangfire;

namespace Gymopedia.Controllers
{
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
                var Name = Context.GetUserFullName();  
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
            RowButton("Мои записи", Q(MyListOfSessions));
            RowButton("Ближайшая тренировка", Q(NearestSession));
            RowButton("Посмотреть иссторию сессий", Q(SessionHistory));
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
            if (!list.Any())
            {
                PushL("Вы ни на кого не подписаны");
                RowButton("Найти тренера", Q(FindCoach));
                RowButton("Вернуться в меню", Q(ClientMenu));
            }
            else
            {
                PushL("Вы подписаны на:");
                foreach (var item in list)
                {
                    var requestCoach = new GetCoach.Request(item.CoachId);
                    var getCoachResponse = await _mediator.Send(requestCoach);
                    var coach = getCoachResponse.Coach;
                    RowButton(coach.Name, Q(CoachMenu, coach));
                }
                RowButton("Вернуться в меню", Q(ClientMenu));
            }
        }

        [Action]
        public async Task CoachMenu(Coach coach)
        {
            PushL($"Тренер: {coach.Name}");
            RowButton("Посмореть информацию", Q(AboutCoach, coach));
            RowButton("Сессии", Q(CoachListOfSessions, coach));
            RowButton("Отписаться", Q(Unsubscribe, coach));
            RowButton("Вернуться в меню", Q(ClientMenu));
        }

        [Action]
        public async Task AboutCoach(Coach coach)
        {
            PushL($"Имя: {coach.Name}");


            RowButton("Назад", Q(CoachMenu, coach));
            RowButton("Вернуться в меню", Q(ClientMenu));
        }

        [Action]
        public async Task MyListOfSessions()
        {
            var chatId = Context.UserId();
            var request = new GetAllClientToSession.Request(chatId);
            var ListResponse = await _mediator.Send(request);
            var list = ListResponse.ClientToSessionList;
            if (!list.Any())
            {
                PushL("У вас нет активных сессий");
                RowButton("Вернуться в меню", Q(ClientMenu));
            }
            else
            {
                PushL("Вы записаны на:");
                foreach (var item in list)
                {
                    var requestSession = new GetSession.Request((int)item.SessionId);
                    var getSessionResponse = await _mediator.Send(requestSession);
                    var session = getSessionResponse.Session;
                    if(session != null)
                    RowButton($"{session.From}", Q(SessionInfo, session));
                }
                RowButton("Вернуться в меню", Q(ClientMenu));
            }
        }

        [Action]
        public async Task SessionHistory()
        {
            var chatId = Context.UserId();
            var request = new GetAllClientToSession.Request(chatId);
            var ListResponse = await _mediator.Send(request);
            var list = ListResponse.ClientToSessionList;
            if (!list.Any())
            {
                PushL("Вы никогда не были записаны на какую-либо сессию");
                RowButton("Вернуться в меню", Q(ClientMenu));
            }
            else
            {
                PushL("Вы были записаны на:");
                foreach (var item in list)
                {
                    var requestSession = new GetSession.Request((int)item.SessionId);
                    var getSessionResponse = await _mediator.Send(requestSession);
                    var session = getSessionResponse.Session;
                    if (session != null)
                        RowButton($"{session.From}", Q(SessionInfo, session));
                }
                RowButton("Вернуться в меню", Q(ClientMenu));
            }
        }

        [Action]
        public async Task SessionInfo(Session session)
        {
            PushL($"Дата: {session.From.ToLocalTime()}");
            var request = new GetCoach.Request(session.CoachId);
            var getCoachResponse = await _mediator.Send(request);
            var coach = getCoachResponse.Coach;
            PushL($"Тренер:{coach.Name}");

            ScheduleAction(new SessionDto
            {
                Id = session.Id,
                CoachId = coach.Id,
                From = session.From
            });

            if (session.From > DateTime.Now.AddMinutes(5))
            {
                RowButton("Отменить запись", Q(UnSubscribeToSessionButton, session));
            }
            RowButton("Вернуться в меню", Q(ClientMenu));
        }



        [Action]
        public async Task CoachListOfSessions(Coach coach)
        {
            var request = new GetActualSessionsByCoachId.Request(coach.ChatId);
            var ListResponse = await _mediator.Send(request);
            var list = ListResponse.SessionList;
            if (!list.Any())
            {
                PushL("У тренера нет свободных сессий");
                RowButton("Вернуться в меню", Q(ClientMenu));
            }
            else
            {
                PushL("Доступные сессии:");
                foreach (var item in list)
                {
                    RowButton($"{item.From.ToLocalTime()}", Q(SessionMenu, item)); 
                }
                RowButton("Вернуться в меню", Q(ClientMenu));
            }
        }
        [Action]
        public async Task SessionMenu(SessionDto session)
        {
            PushL($"Дата: {session.From}");
            RowButton("Записаться", Q(SubscribeToSessionButton, session));
            RowButton("Вернуться в меню", Q(ClientMenu));
        }

        [Action]
        public async Task SubscribeToSessionButton(SessionDto session)
        {
            var chatId = Context.UserId();

            var clientToSession = await GetClientToSessoin(chatId, session.Id);

            if(clientToSession == null)
            {
                await SubscribeToSession(chatId, session.Id);
                PushL("Вы успешно записались");
                var _sendler = new Sendler(Context.Bot.Client);
                _sendler.Exec("Клиент записался на сессию", session.CoachId);

                ScheduleAction(session);
            } else PushL("Вы уже записаны");
            RowButton("Вернуться в меню", Q(ClientMenu));
        }

        [Action]
        public async Task UnSubscribeToSessionButton(Session session)
        {
            var chatId = Context.UserId();
            await DeleteClientToSessoin(chatId, session.Id);
            PushL("Вы отменили запись");
            RowButton("Вернуться в меню", Q(ClientMenu));
            var _sendler = new Sendler(Context.Bot.Client);
            _sendler.Exec("Клиент отменил запись", session.CoachId);
        }

        [Action]
        public async Task NearestSession()
        {
            var chatId = Context.UserId();
            var request = new GetNearestSession.Request(chatId);
            var Response = await _mediator.Send(request);
            var session = Response.Session;
            if (session == null)
            {
                PushL("У вас нет ближайших тренировок");
                RowButton("Вернуться в меню", Q(ClientMenu));
            }
            else
            {
                SessionInfo(session);
            }
        }


        [Action]
        public async Task FindCoach()
        {
            PushL("Введите имя теренера или id");
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
            RowButton("Найти по имени", Q(FindByName));
            RowButton("Найти по id", Q(FindById));
            RowButton("Вернуться в меню", Q(ClientMenu));
        }



        [Action]
        async Task FindByName()
        {
            var fillState = await GetAState<FillState>();
            string Name = fillState.Comment;
            if (!string.IsNullOrEmpty(Name))
            {
                Name = Name.Trim(new char[] { '@' });
            }
            var coach = await GetByName(Name);

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
        async Task FindById()
        {
            var fillState = await GetAState<FillState>();
            string numberStr = fillState.Comment;
            long chatId;
            bool isParsable = long.TryParse(numberStr, out chatId);
            Coach coach = null;
            if (isParsable)
            {
                var request = new GetCoach.Request(chatId);
                var getCoachResponse = await _mediator.Send(request);
                coach = getCoachResponse.Coach;
            }
            if (coach != null)
            {
                PushL(coach.Name);
                PushL(coach.Id.ToString());
                PushL(coach.ChatId.ToString());

                RowButton("подписаться", Q(Subscribe, coach));
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
            var subscribeToCoach = await GetClientToCoach(chatId, coach.ChatId);
            if(subscribeToCoach == null)
            {
                await SubscribeToCoach(chatId, coach.ChatId);
                PushL($"Вы подписались на {coach.Name}");
            } else
            PushL($"Вы уже подписанны на {coach.Name}");
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
        public async Task ScheduleAction(SessionDto session)
        {
            var model = new Remind(Client, session, _mediator);
            var job = BackgroundJob.Schedule<SessionReminder>(c => c.SendRemind(model), TimeSpan.FromSeconds(5));
        }

        public async Task<List<ClientToSession>> GetAllClientsBySession(int sessionId)
        {
            var request = new GetAllClientsBySession.Request(sessionId);
            var ListResponse = await _mediator.Send(request);
            return ListResponse.ClientToSessionList;
        }

        #region ClientToSession
        public async Task SubscribeToSession(long clientId, int sessionId)
        {
            var request = new SubscribeToSession.Request(clientId, sessionId);
            var subscribeToSessionResponse = await _mediator.Send(request);
        }
        public async Task<ClientToSession> GetClientToSessoin(long clientId, int sessionId)
        {
            var request = new GetClientToSession.Request(clientId, sessionId);

            var getClientToSessionResponse = await _mediator.Send(request);

            return getClientToSessionResponse.ClientToSession;
        }
        public async Task DeleteClientToSessoin(long clientId, int sessionId)
        {
            var request = new DeleteClientToSession.Request(clientId, sessionId);

            var deleteClientToSessionResponse = await _mediator.Send(request);

        }
        public async Task<Session> GetSession(int sessionId, CancellationToken cancellationToken)
        {
            var request = new GetSession.Request(sessionId);

            var getSessionResponse = await _mediator.Send(request, cancellationToken);

            return getSessionResponse.Session;
        }

        #endregion

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
        public async Task<ClientToCoach> GetClientToCoach(long clientId, long coachId)
        {
            var request = new GetClientToCoach.Request(clientId, coachId);
            var getClientToCoachResponse = await _mediator.Send(request);
            return getClientToCoachResponse.ClientToCoach;
        }
        [HttpDelete]
        [Route("/deleteClientToCoach")]
        public async Task DeleteClientToCoach(long clientId, long coachId)
        {
            var request = new DeleteClientToCoach.Request(clientId, coachId);

            var deleteClientToCoachResponse = await _mediator.Send(request);

        }
        #endregion

        #region Client

        [Action]
        public async Task<Coach> GetByName(string Name)
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
