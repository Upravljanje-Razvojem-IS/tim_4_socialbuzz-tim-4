using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models.Requests
{
    public class LogoutRequest
    {
        /// <summary>
        /// Token korisnika koji želi da se odjavi
        /// </summary>
        public string Token { get; set; }
    }
}
