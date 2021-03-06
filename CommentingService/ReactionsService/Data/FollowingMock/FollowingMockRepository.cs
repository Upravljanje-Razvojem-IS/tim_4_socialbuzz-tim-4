﻿using ReactionsService.Models.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Data.FollowingMock
{
    public class FollowingMockRepository : IFollowingMockRepository
    {
        public static List<FollowingDto> FollowingUsers { get; set; } = new List<FollowingDto>();

        public FollowingMockRepository()
        {
            FillData();
        }

        private void FillData()
        {

            FollowingDto f = new FollowingDto();
            f.FollowingID = 1;
            f.FollowerID = 1;
            f.FollowedID = 3;

            FollowingUsers.Add(f);
        }

        public List<int> GetFollowedUsers(int userId)
        {
            List<int> listOfFollowedUsers = new List<int>();

            var query = from l1 in FollowingUsers
                        select l1;

            foreach (var v in query)
            {
                if (v.FollowerID == userId)
                {
                    listOfFollowedUsers.Add(v.FollowedID);
                }

            }

            return listOfFollowedUsers;
        }

        public bool CheckDoIFollowUser(int userId, int followingId)
        {

            var query = from l1 in FollowingUsers
                        select l1;

            foreach (var v in query)
            {
                if (v.FollowerID == userId && v.FollowedID == followingId)
                {
                    return true;
                }

            }

            return false;
        }
    }
}
