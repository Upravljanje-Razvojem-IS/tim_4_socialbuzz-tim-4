using ReactionsService.Models.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Data.PostMock
{
    public class PostMockRepository : IPostMockRepository
    {
        public static List<PostDto> Posts { get; set; } = new List<PostDto>();

        public PostMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Posts.AddRange(new List<PostDto>
            {
                new PostDto
                {
                    PostID = 1,
                    ProductID = 1,
                    ProductName = "Samsung Galaxy A52",
                    UserID = 1,
                    Description = "Povoljno prodajem Samsung Galaxy A52, kao nov!",
                    Price = "350e",
                    Quantity = 1,
                    PostedOn = DateTime.Parse("2021-04-21T09:00:00")
                },
                new PostDto
                {
                   PostID = 2,
                   ProductID = 2,
                   ProductName = "Delimano posudje",
                   UserID = 2,
                   Description = "Oglasavam prodaju seta Delimano posudja, nekorisceno",
                   Price = "12000din",
                   Quantity = 3,
                   PostedOn = DateTime.Parse("2021-04-21T09:00:00")
                },
                 new PostDto
                {
                    PostID = 3,
                    ProductID = 3,
                    ProductName = "Lenovo IdeaPad 520",
                    UserID = 3,
                    Description = "U prodaji polovan i ocuvan Lenovo ideapad520",
                    Price = "220e",
                    Quantity = 1,
                    PostedOn = DateTime.Parse("2021-04-21T09:00:00")
                }
            });
        }

        public PostDto GetPostById(int postId)
        {
            return Posts.FirstOrDefault(e => e.PostID == postId);
        }
    }
}
