using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Models.MocksDto
{
    /// <summary>
    /// Mock-ovani dto model korisnika
    /// </summary>
    public class UserDto
    {
        
        /// <summary>
        /// Id korisnika
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Ime korisnika
        /// </summary>
        public String FirstName { get; set; }

        /// <summary>
        /// Prezime korisnika
        /// </summary>
        public String LastName { get; set; }

        /// <summary>
        /// Username korisnika
        /// </summary>
        public String Username { get; set; }

        /// <summary>
        /// Email korisnika
        /// </summary>
        public String Email { get; set; }

        /// <summary>
        /// Broj telefona korisnika
        /// </summary>
        public String PhoneNumber { get; set; }

        /// <summary>
        /// Grad odakle je korisnik
        /// </summary>
        public String City { get; set; }

        /// <summary>
        /// Uloga korisnika (admin, user)
        /// </summary>
        public String Role { get; set; }

        /// <summary>
        /// Da li je trenutno aktivan korisnik na mrezi
        /// </summary>
        public Boolean IsActive { get; set; }
    }
}
