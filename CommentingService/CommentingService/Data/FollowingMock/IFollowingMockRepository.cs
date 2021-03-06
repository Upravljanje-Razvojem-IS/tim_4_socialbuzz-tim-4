﻿namespace CommentingService.Data.FolllowingMock
{
    using System.Collections.Generic;


    public interface IFollowingMockRepository
    {
        List<int> GetFollowedUsers(int userId);

        /// <summary>
        /// Provera da li pratim korisnika
        /// </summary>
        /// <param name="userId">Moj Id</param>
        /// <param name="followingId">Id korisnika za kog proveravam da li ga pratim</param>
        /// <returns></returns>
        public bool CheckDoIFollowUser(int userId, int followingId);
    }
}
