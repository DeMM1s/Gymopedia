﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymopedia.Domain.DtoModels
{
    public class ClientToSessionDTO
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int ClientId { get; set; }
    }
}
