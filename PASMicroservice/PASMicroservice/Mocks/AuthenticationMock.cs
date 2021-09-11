using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace PASMicroservice.Mocks
{
    public class AuthenticationMock : IAuthenticationMock
    {
        private readonly IConfiguration configuration;
        private readonly IUserMockRepository userMockRepository;

        public AuthenticationMock(IConfiguration configuration, IUserMockRepository userMockRepository)
        {
            this.configuration = configuration;
            this.userMockRepository = userMockRepository;
        }

        public bool AuthenticateUser(UserDto user)
        {
            if (this.userMockRepository.GetUserById(user.Id) != null)
                return true;

            return false;
        }

        public string GenerateJwt(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                                             configuration["Jwt:Issuer"],
                                             null,
                                             expires: DateTime.Now.AddMinutes(120),
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
