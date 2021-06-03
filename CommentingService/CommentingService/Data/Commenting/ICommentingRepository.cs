namespace CommentingService.Data.Commenting
{
    using CommentingService.Entities;
    using System;
    using System.Collections.Generic;

    public interface ICommentingRepository
    {
        List<Comment> GetAllComments();

        Comment GetCommentByID(Guid commentID);

        List<Comment> GetCommentsByPostID(int postID, int userID);

        void CreateComment(Comment comment);

        void UpdateComment(Comment comment);

        void DeleteComment(Guid commentID);

        bool CheckDoIFollowUser(int userID, int followingID);

        bool CheckDidIBlockUser(int userID, int blockedID);

        public bool SaveChanges();
    }
}
