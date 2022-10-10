﻿using Gymopedia.Infrastructure.Exceptions;

namespace Gymopedia.Domain.Models
{
    public class Session
    {
        public int Id { get; init; }
        public DateTime From { get; init; }
        public DateTime Until { get; init; }
        public int CoachWorkDayIdOwner { get; init; }

        private int _maxClient;
        public Session()
        {

        }
        public Session(DateTime from, DateTime until, int maxClient, int coachWorkDayId)
        {
            From = from;
            Until = until;
            _maxClient = maxClient;
            CoachWorkDayIdOwner = coachWorkDayId;
        }


        public void SetMaxClient(int MaxClient)
        {
            if (MaxClient < 1 || MaxClient > 30)
            {
                throw new ValidationException("Клиентов для одной сессии должно быть не меньше 1 и не больше 30");
            }

            _maxClient = MaxClient;
        }
       
    }
}