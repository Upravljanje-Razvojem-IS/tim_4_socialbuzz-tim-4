using ReactionsService.Models.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Data.PostMock
{
    public interface IPostMockRepository
    {
        PostDto GetPostById(int postId);
    }
}
