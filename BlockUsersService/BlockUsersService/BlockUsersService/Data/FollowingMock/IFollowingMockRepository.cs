using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Data.FollowingMock
{
    public interface IFollowingMockRepository
    {
        public bool FollowingUser(int userID, int followingID);

    }
}
