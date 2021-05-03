using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.AuthorizationHelper
{
    public interface IAuthorization
    {
        public bool AuthorizeUser(string key);
    }
}
