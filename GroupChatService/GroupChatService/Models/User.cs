using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChatService.Models
{
    public class User : IdentityUser
    {
        public static object Identity { get; internal set; }
        public ICollection<ChatUser> Chats { get; set; }
    }
}
