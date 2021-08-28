using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Models.MocksDto
{
    /// <summary>
    /// Mock-ovani dto model pracenja korisnika
    /// </summary>
    public class FollowingDto
    {

        /// <summary>
        /// Id pracenja
        /// </summary>
        public int ID { get; set; }


        /// <summary>
        /// Korisnik koji prati drugog korisnika
        /// </summary>
        public int FollowerID { get; set; }


        /// <summary>
        /// Korisnik koji je zapracen
        /// </summary>
        public int FollowedID { get; set; }

    }
}
