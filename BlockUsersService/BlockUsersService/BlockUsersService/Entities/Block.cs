using System;
using System.Collections.Generic;

#nullable disable

namespace BlockUsersService.Entities
{
    public partial class Block
    {
        public Guid Id { get; set; }
        public int BlockerId { get; set; }
        public int BlockedId { get; set; }
        public DateTime? BlockDate { get; set; }
    }
}
