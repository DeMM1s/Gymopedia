using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymopedia.Domain.DtoModels
{
    public class SessionDto
    {
        public int Id { get; init; }
        public DateTime From { get; init; }
        public DateTime Until { get; init; }
        public long CoachId { get; init; }
        public int ClientCount { get; init; }

        public int MaxClient;
    }
}
