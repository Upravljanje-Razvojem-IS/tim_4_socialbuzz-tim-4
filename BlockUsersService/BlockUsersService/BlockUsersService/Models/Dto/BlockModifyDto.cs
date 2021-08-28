using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Models.Dto
{
    /// <summary>
    /// Dto model za modifkaciju blokiranja
    /// </summary>
    public class BlockModifyDto
    {

        /// <summary>
        /// Id blokiranja
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Korisnik koji blokira drugog korisnika
        /// </summary>
        [Required(ErrorMessage = "Blocker ID is required, please write it!")]
        public int BlockerId { get; set; }

        /// <summary>
        /// Korisnik koji je blokiran
        /// </summary>
        [Required(ErrorMessage = "Blocked ID is required, please write it!")]
        public int BlockedId { get; set; }
    }
}
