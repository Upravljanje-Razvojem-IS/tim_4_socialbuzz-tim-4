using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Models.MocksDto
{
    public class FollowingDto
    {
        public int ID { get; set; }

        public int FollowerID { get; set; }

        public int FollowedID { get; set; }

    }
}
