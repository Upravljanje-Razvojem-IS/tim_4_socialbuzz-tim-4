using ReactionsService.Data.BlockingMock;
using ReactionsService.Data.FollowingMock;
using ReactionsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Data.Reactions
{
    public class ReactionRepository : IReactionRepository
    {
        private readonly ContextDB contextDB;
        private readonly IBlockingMockRepository blockingMockRepository;
        private readonly IFollowingMockRepository followingMockRepository;

        public ReactionRepository(ContextDB contextDB, IBlockingMockRepository blockingMockRepository, IFollowingMockRepository followingMockRepository)
        {
            this.contextDB = contextDB;
            this.blockingMockRepository = blockingMockRepository;
            this.followingMockRepository = followingMockRepository;
        }

        public Reaction CheckDidIAlreadyReact(int userID, int postID)
        {
            return contextDB.Reactions.FirstOrDefault(e => e.UserID == userID && e.PostID == postID);

        }

        public bool CheckDidIBlockUser(int userID, int blockedID)
        {
            return blockingMockRepository.CheckDidIBlockUser(userID, blockedID);
        }

        public bool CheckDoIFollowUser(int userID, int followingID)
        {
            return followingMockRepository.CheckDoIFollowUser(userID, followingID);
        }

        public void CreateReaction(Reaction reaction)
        {
            contextDB.Add(reaction);
        }

        public void DeleteReaction(Guid reactionID)
        {
            var reaction = GetReactionByID(reactionID);
            contextDB.Remove(reaction);
        }

        public List<Reaction> GetAllReactions()
        {
            return contextDB.Reactions.ToList();
        }

        public Reaction GetReactionByID(Guid reactionID)
        {
            return contextDB.Reactions.FirstOrDefault(e => e.ReactionID == reactionID);
        }

        public List<Reaction> GetReactionByPostID(int postID, int userID)
        {
            var query = from reaction in contextDB.Reactions
                        where !(from o in blockingMockRepository.GetBlockedUsers(userID)
                                select o).Contains(reaction.UserID)
                        where reaction.PostID == postID
                        select reaction;

            return query.ToList();
        }

        public bool SaveChanges()
        {
            return contextDB.SaveChanges() > 0;
        }

        public void UpdateReaction(Reaction reaction)
        {
            //Mapiranje
        }
    }
}
