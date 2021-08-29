using BlockUsersService.Models.MocksDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Data.FollowingMock
{
    public class FollowingMockRepository : IFollowingMockRepository
    {
        public FollowingMockRepository()
        {
            FillData();
        }

        public static List<FollowingDto> FollowingUsersList { get; set; } = new List<FollowingDto>();

        private void FillData()
        {
            FollowingDto dto = new FollowingDto(); // User sa id=1 prati user-a sa id=2
            dto.ID = 1;
            dto.FollowerID = 1;
            dto.FollowedID = 2;

            FollowingDto dto2 = new FollowingDto(); // User sa id=2 prati user-a sa id=3
            dto2.ID = 2;
            dto2.FollowerID = 2;
            dto2.FollowedID = 3;

            FollowingDto dto3 = new FollowingDto(); // User sa id=3 prati user-a sa id=4
            dto3.ID = 3;
            dto3.FollowerID = 3;
            dto3.FollowedID = 4;

            FollowingDto dto4 = new FollowingDto(); // User sa id=4 prati user-a sa id=5
            dto4.ID = 4;
            dto4.FollowerID = 4;
            dto4.FollowedID = 5;

            FollowingDto dto5 = new FollowingDto(); // User sa id=2 prati user-a sa id=5
            dto5.ID = 5;
            dto5.FollowerID = 2;
            dto5.FollowedID = 5;

            FollowingUsersList.Add(dto);
            FollowingUsersList.Add(dto2);
            FollowingUsersList.Add(dto3);
            FollowingUsersList.Add(dto4);
            FollowingUsersList.Add(dto5);

        }
        public bool FollowingUser(int userID, int followingID)
        {
            var list = from l in FollowingUsersList
                       select l;

            foreach (var item in list)
            {
                if (item.FollowerID == userID && item.FollowedID == followingID)
                {
                    return true;
                }

            }

            return false;
        }

        
    }
}
