
namespace Gymopedia.Infrastructure.Constants
{
    public static class Constants
    {
        public static class ErrorMessage
        {
            public static class Coach
            {
                public const string CoachNotFoundError = "Тренер с указанным id не найден";
            }
            public static class Client
            {
                public const string ClientNotFoundError = "Клиент с указанным id не найден";
            }
            public static class CoachWorkDay
            {
                public const string CoachWorkDayNotFoundError = "День с указанным id не найден";
            }
            public static class Session
            {
                public const string SessionNotFoundError = "Сессия с указанным id не найдена";
            }
        }
    }
}