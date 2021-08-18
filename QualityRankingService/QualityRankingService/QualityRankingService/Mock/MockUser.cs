using System;
using System.Collections.Generic;

namespace QualityRanking.Mock
{
    public static class MockUser
    {
        public readonly static List<User> Users = new List<User>()
        {
            new User()
            {
                Id = Guid.Parse("51ad74c3-7189-4ac3-ab7a-2e4f0faf9d32"),
                FirstName = "Nikola",
                LastName = "Nikolic"
            },
            new User()
            {
                Id = Guid.Parse("68477a54-8afc-42c3-99ad-9a517873f7ca"),
                FirstName = "Pera",
                LastName = "Peric"
            }
        };
    }

    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
