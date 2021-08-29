using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerService.AuthHelper
{
    public interface IAuthHelperr
    {
        public bool AuthUser(string secretKey);
    }
}
