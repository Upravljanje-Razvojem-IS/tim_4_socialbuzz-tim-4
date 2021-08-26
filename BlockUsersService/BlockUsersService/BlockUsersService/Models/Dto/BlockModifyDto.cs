using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Models.Dto
{
    public class BlockModifyDto
    {
        public Guid Id { get; set; }
        public int BlockerId { get; set; }
        public int BlockedId { get; set; }
    }
}
