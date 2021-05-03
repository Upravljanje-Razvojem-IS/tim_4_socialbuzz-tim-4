using ReactionsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Data.Reactions
{
    public interface IReactionRepository
    {
        List<Reaction> GetAllReactions();

        List<Reaction> GetReactionByPostID(int postID, int userID);

        public Reaction GetReactionByID(Guid reactionID);

        void CreateReaction(Reaction reaction);

        public void UpdateReaction(Reaction reaction);

        public void DeleteReaction(Guid reactionID);

        public Reaction CheckDidIAlreadyReact(int userID, int postID);

        bool CheckDoIFollowUser(int userID, int followingID);

        bool CheckDidIBlockUser(int userID, int blockedID);

        bool SaveChanges();
    }
}
