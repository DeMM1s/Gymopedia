﻿using Gymopedia.Domain.Models;

namespace Gymopedia.Domain.DtoModels
{
    public class CoachDto
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public ICollection<Client> Clients { get; init; } = new List<Client>();

        public MyCalendar? Calendar { get; init; }
    }
}
