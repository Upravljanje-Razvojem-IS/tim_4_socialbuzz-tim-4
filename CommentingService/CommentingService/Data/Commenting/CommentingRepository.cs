namespace CommentingService.Data.Commenting
{
    using CommentingService.Data.BlockingMock;
    using CommentingService.Data.FolllowingMock;
    using CommentingService.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CommentingRepository : ICommentingRepository
    {

        private readonly ContextDB context;
        private readonly IFollowingMockRepository followingMockRepository;
        private readonly IBlockingMockRepository blockingMockRepository;
        
        public CommentingRepository(ContextDB contextDB, IFollowingMockRepository followingMockRepository, IBlockingMockRepository blockingMockRepository)
        {
            context = contextDB;
            this.followingMockRepository = followingMockRepository;
            this.blockingMockRepository = blockingMockRepository;
        }

        public List<Comment> GetAllComments()
        {
            return context.Comments.ToList();
        }

        public Comment GetCommentByID(Guid commentID)
        {
            return context.Comments.FirstOrDefault(e => e.CommentID == commentID);
        }

        public List<Comment> GetCommentsByPostID(int postID, int userID)
        {
            var query = from comment in context.Comments
                        where !(from o in blockingMockRepository.GetBlockedUsers(userID)
                                select o).Contains(comment.UserID)
                        where comment.PostID == postID
                        select comment;

            return query.ToList();
        }

        public void CreateComment(Comment comment)
        {
            context.Comments.Add(comment);
        }

        public void DeleteComment(Guid commentID)
        {
            var comment = GetCommentByID(commentID);
            context.Remove(comment);
        }

        public bool CheckDidIBlockUser(int userId, int blockedId)
        {
            return blockingMockRepository.CheckDidIBlockUser(userId, blockedId);
        }

        public bool CheckDoIFollowUser(int userId, int followingId)
        {
            return followingMockRepository.CheckDoIFollowUser(userId, followingId);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateComment(Comment comment)
        {
            //context.Update(comment); --> mapiranje old na new comment u kontoleru!
        }
    }
}
