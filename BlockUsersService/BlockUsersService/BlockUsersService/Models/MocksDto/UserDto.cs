using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Models.MocksDto
{
    public class UserDto
    {
        
        public int ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public String City { get; set; }
        public String Role { get; set; }
        public Boolean IsActive { get; set; }
    }
}
