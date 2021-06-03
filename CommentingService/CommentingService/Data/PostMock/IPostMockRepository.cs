namespace CommentingService.Data.PostMock
{
    using CommentingService.Models;

    public interface IPostMockRepository
    {

        PostDto GetPostById(int postId);
    }
}
