﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.AuthorizationHelper
{
    public class Authorization : IAuthorization
    {
        private readonly IConfiguration configuration;

        public Authorization(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool AuthorizeUser(string key)
        {
            if (!key.StartsWith("Bearer"))
            {
                return false;
            }

            var keyOnly = key.Substring(key.IndexOf("Bearer") + 7);
            var storedKey = configuration.GetValue<string>("Authorization:Key");

            if (storedKey != keyOnly)
            {
                return false;
            }
            return true;
        }
    }
}
