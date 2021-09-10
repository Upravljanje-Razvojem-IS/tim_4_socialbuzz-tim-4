using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Mocks
{
    public class UserMockRepository : IUserMockRepository
    {
        public static List<UserDto> Users { get; set; } = new List<UserDto>();

        public UserMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Users.AddRange(new List<UserDto>
            {
                new UserDto
                {
                    Id = 1337,
                    Username = "katicstefan",
                    FirstName = "Stefan",
                    LastName = "Katic",
                    Contact = "067123456"
                },
                new UserDto
                {
                    Id = 1338,
                    Username = "ppetrovic21",
                    FirstName = "Petar",
                    LastName = "Petrovic",
                    Contact = "068123456"
                },
                new UserDto
                {
                    Id = 1338,
                    Username = "jovanj19",
                    FirstName = "Jovan",
                    LastName = "Jovanovic",
                    Contact = "068123456"
                },
                new UserDto
                {
                    Id = 1340,
                    Username = "tester",
                    FirstName = "Test",
                    LastName = "Testington",
                    Contact = "060123456"
                }
            });
        }

        public UserDto GetUserById(int userId)
        {
            return Users.FirstOrDefault(e => e.Id == userId);
        }
    }
}
