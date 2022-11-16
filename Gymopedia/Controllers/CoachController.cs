using Microsoft.AspNetCore.Mvc;
using Gymopedia.Domain.DtoModels;
using Gymopedia.Core.Coaches;
using Gymopedia.Core.Sessions;
using Gymopedia.Core.ClientToSessions;
using MediatR;
using Deployf.Botf;
using Gymopedia.Domain.Models;
using Gymopedia.Core.Telegram;

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
        [Action]
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
            RowButton("Создать", Q(SessionCreator));
            RowButton("Актуальные сессии", Q(ListOfSessions));
            RowButton("Вернуться в меню", Q(CoachMenu));
            RowButton("Посмотреть историю сессий", Q(HistorySession));
        }
        [Action]
        public async Task ListOfSessions()
        {
            var chatId = Context.UserId();
            var request = new GetActualSessionsByCoachId.Request(chatId);
            var ListResponse = await _mediator.Send(request);
            var list = ListResponse.SessionList;
            if (!list.Any())
            {
                PushL("У вас нет сессий");
                RowButton("Создать", Q(SessionCreator));
                RowButton("Вернуться в меню", Q(CoachMenu));
            }
            else
            {
                PushL("Ваши сессии:");
                foreach (var item in list)
                {
                    RowButton($"{item.From.ToLocalTime()}, клиентов: {item.ClientCount}", Q(SessionMenu, item));
                }
                RowButton("Вернуться в меню", Q(CoachMenu));
            }
        }

        [Action]
        public async Task HistorySession()
        {
            var chatId = Context.UserId();
            var request = new GetHistoryByCoachId.Request(chatId);
            var ListResponse = await _mediator.Send(request);
            var list = ListResponse.SessionList;
            if (!list.Any())
            {
                PushL("У вас не было ни одной сессии");
                RowButton("Создать", Q(SessionCreator));
                RowButton("Вернуться в меню", Q(CoachMenu));
            }
            else
            {
                PushL("Ваши сессии:");
                foreach (var item in list)
                {
                    RowButton($"{item.From.ToLocalTime()}, клиентов: {item.ClientCount}", Q(SessionMenu, item));
                }
                RowButton("Вернуться в меню", Q(CoachMenu));
            }
        }

        [Action]
        async Task SessionMenu(SessionDto session)
        {
            PushL($"{session.From.ToLocalTime()}, клиентов: {session.ClientCount}, id: {session.Id}");
            RowButton("отменить сессию", Q(DeleteSessionButton, session));
            RowButton("назад", Q(ListOfSessions));
            RowButton("Вернуться в меню", Q(CoachMenu));
        }

        [Action]
        async Task DeleteSessionButton(SessionDto session)
        {
            PushL("Расскажите о причине отмены");
            Fill_Comment(await GetAState<CoachState>(), session);
            await Send();
        }

        [Action]
        async Task DeleteSessionActoin(SessionDto session)
        {
            await DeleteSession(session.Id);
            PushL("Вы отменили сессию");
            RowButton("Вернуться в меню", Q(CoachMenu));

            var request = new GetAllClientsBySession.Request(session.Id);
            var ListResponse = await _mediator.Send(request);
            var list = ListResponse.ClientToSessionList;
            if (list.Any())
            {
                var fillState = await GetAState<CoachState>();
                string comment = fillState.Comment;
                if (string.IsNullOrEmpty(comment))
                {
                    comment = "Тренер молча отменил сессию";
                }
                else comment = $"Тренер отменил сессию c комментарием: {comment}";
                var _sendler = new Sendler(Context.Bot.Client);
                PushL(comment);
                foreach (var item in list)
                {
                    _sendler.Exec(comment, item.ClientId);
                }
            }
        }

        [Action]
        async Task SessionCreator()
        {
            PushL("дейсвия");
            RowButton("указать время", Q(Fill_LoopTime,"."));
            var fillState = await GetAState<CoachState>();
            if (fillState.IsSet)
            {
                RowButton("готово", Q(FillSession, ""));
            }
        }
        [Action]
        async Task FillSession([State] CoachState state)
        {
            PushL("время:");
            PushL(state.Time.Value.ToString());
            var chatId = Context.UserId();
            await CreateSession(state.Time.Value.ToUniversalTime(), chatId);
            PushL("сессия создана");
            RowButton("Вернуться в меню", Q(CoachMenu));
        }


        [Action]
        public async Task NearestClient()
        {
            var chatId = Context.UserId();
            var request = new GetNearestClient.Request(chatId);
            var Response = await _mediator.Send(request);
            var session = Response.Session;
            if (session == null)
            {
                PushL("У вас нет ближайших тренировок");
                RowButton("Вернуться в меню", Q(CoachMenu));
            }
            else
            {
                SessionMenu(session);
            }
        }

        [Action]
        void Fill_LoopTime(string state)
        {
            Push("Выберете время начала");

            var now = DateTime.Now;

            Calendar().Depth(CalendarDepth.Time)
                .SetState(state)

                .OnNavigatePath(s => Q(Fill_LoopTime, s))
                .OnSelectPath((d, s) => Q(Fill_SetTime, d, ""))
                .Build(Message);
        }

        CalendarMessageBuilder Calendar()
        {
            var now = DateTime.Now;
            return new CalendarMessageBuilder()
                .Year(now.Year)
                .Month(now.Month)
                .Day(now.Day);
        }

        [Action]
        async ValueTask Fill_SetTime(DateTime start, [State] CoachState state)
        {
            state = state with { Time = start };
            await AState(state);
            await SessionCreator();
        }


        #region EnterInfromation
        [Action]
        void Fill_Comment([State] CoachState state, SessionDto session)
        {
            State(new SetCoachState());
            RowButton("Внести комментарий и отменить сессию", Q(DeleteSessionActoin, session));
            RowButton("Вернуться в меню", Q(CoachMenu));
        }
        [State]
        async ValueTask Fill_StateComment(SetCoachState state)
        {
            var fillState = await GetAState<CoachState>();
            fillState = fillState with { Comment = Context.GetSafeTextPayload() };
            await AState(fillState);
        }
        record SetCoachState;
        struct CoachState
        {
            public string? Comment { get; set; }
            public DateTime? Time { get; set; }
            public bool IsSet => Time.HasValue;
        }
        #endregion

        #region session
        public async Task CreateSession(DateTime From, long CoachId)
        {
            var request = new CreateSession.Request(From, CoachId);
            var createSessionResponse = await _mediator.Send(request);

        }

        [HttpGet]
        [Route("/getSession")]
        public async Task Get(int sessionId, CancellationToken cancellationToken)
        {
            var request = new GetSession.Request(sessionId);

            var getSessionResponse = await _mediator.Send(request, cancellationToken);

        }

        [HttpPut]
        [Route("/editSession")]
        public async Task Edit(Session session, CancellationToken cancellationToken)
        {
            var request = new EditSession.Request(session);

            var editSessionResponse = await _mediator.Send(request, cancellationToken);

        }



        [HttpDelete]
        [Route("/deleteSession")]
        public async Task DeleteSession(int sessionId)
        {
            var request = new DeleteSession.Request(sessionId);

            var deleteSessionResponse = await _mediator.Send(request);

        }
        #endregion

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
