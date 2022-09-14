using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymopedia.Core.Models
{
    public class SessionDto
    {
        public int Id { get; init; }
        public DateTime From { get; init; }
        public DateTime Until { get; init; }
        public int CoachWorkDayId { get; init; }
        public ICollection<int> ClientIds { get; init; } = new List<int>();

        public int MaxClient;
    }
}
