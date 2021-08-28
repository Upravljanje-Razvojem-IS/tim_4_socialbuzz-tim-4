using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Models.Dto
{
    /// <summary>
    /// Dto model za kreiranje bloka
    /// </summary>
    public class BlockCreationDto
    {
        /// <summary>
        /// Korisnik koji blokira drugog korisnika
        /// </summary>
        [Required(ErrorMessage = "Blocker ID is required, please write it!")]
        public int blockerID { get; set; }


        /// <summary>
        /// Korisnik koji je blokiran
        /// </summary>
        [Required(ErrorMessage = "Blocked ID is required, please write it!")]
        public int blockedID { get; set; }
    }
}
