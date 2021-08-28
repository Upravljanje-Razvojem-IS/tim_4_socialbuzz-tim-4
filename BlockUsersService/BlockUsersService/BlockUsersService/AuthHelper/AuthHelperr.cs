using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.AuthHelper
{
    public class AuthHelperr : IAuthHelper
    {
        private readonly IConfiguration configuration1;
        public AuthHelperr(IConfiguration configuration)
        {
            this.configuration1 = configuration;
        }
        public bool AuthUser(string secretKey)
        {
            if (!secretKey.StartsWith("Bearer")) 
                return false;

            //var lenght = "Bearer ".Length;

            var key = secretKey.Substring(secretKey.IndexOf("Bearer") + 7);
            var storedKey = configuration1.GetValue<string>("Authorization:Key");

            if (storedKey != key) return false;

            return true;
        }
    }
}
