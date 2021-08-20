using BlockUsersService.Models.MocksDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Data.UserMock
{
    public interface IUserMockRepository
    {
        List<UserDto> GetUsers();

        UserDto GetUserById(int ID);
    }
}
