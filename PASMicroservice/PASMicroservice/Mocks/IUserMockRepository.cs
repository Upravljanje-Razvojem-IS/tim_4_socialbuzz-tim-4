using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Mocks
{
    public interface IUserMockRepository
    {
        UserDto GetUserById(int userId);
        bool IsAdmin(int userId);
    }
}
