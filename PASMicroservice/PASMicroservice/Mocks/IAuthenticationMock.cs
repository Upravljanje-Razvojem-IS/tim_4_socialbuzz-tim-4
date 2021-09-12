using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Mocks
{
    public interface IAuthenticationMock
    {
        public bool AuthenticateUser(UserDto user);
        public string GenerateJwt(UserDto user);
    }
}
