using BlockUsersService.Models.MocksDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Data.UserMock
{
    public class UserMockRepository : IUserMockRepository
    {
        public UserMockRepository()
        {
            FillData();
        }

        public static List<UserDto> Users { get; set; } = new List<UserDto>();

        private void FillData() {

            Users.AddRange(new List<UserDto> {

                new UserDto{

                    ID = 1,
                    FirstName = "Milica",
                    LastName = "Denic",
                    Username = "micika98",
                    Email = "milicadenic@gmail.com",
                    PhoneNumber = "063159357",
                    City = "Mladenovac",
                    Role = "User",
                    IsActive = true
                },
                new UserDto{

                    ID = 2,
                    FirstName = "Anja",
                    LastName = "Despotovic",
                    Username = "manjaanja00",
                    Email = "anjadespotovic@gmail.com",
                    PhoneNumber = "064963147",
                    City = "Mladenovac",
                    Role = "User",
                    IsActive = true
                },
                new UserDto{

                    ID = 3,
                    FirstName = "Rumenka",
                    LastName = "Zivkovic",
                    Username = "zivka7",
                    Email = "rumenkazivkovic@gmail.com",
                    PhoneNumber = "0627894567",
                    City = "Novi Sad",
                    Role = "User",
                    IsActive = true
                },
                new UserDto{

                    ID = 4,
                    FirstName = "Nikola",
                    LastName = "Djuric",
                    Username = "nidza10",
                    Email = "nikoladjuric@gmail.com",
                    PhoneNumber = "065297729",
                    City = "Beograd",
                    Role = "User",
                    IsActive = true
                },
                new UserDto{

                    ID = 5,
                    FirstName = "Tara",
                    LastName = "Vitkovic",
                    Username = "takimaki",
                    Email = "taravitkovic@gmail.com",
                    PhoneNumber = "066739148",
                    City = "Novi Sad",
                    Role = "User",
                    IsActive = true
                },
            });
        }

        public UserDto GetUserById(int ID)
        {
            return Users.FirstOrDefault(user => user.ID == ID);
        }

        public List<UserDto> GetUsers()
        {
            return Users;
        }
    }
}
