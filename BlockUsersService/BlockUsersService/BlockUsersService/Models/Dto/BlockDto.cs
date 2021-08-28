using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Models.Dto
{
    /// <summary>
    /// Dto model blokiranja
    /// </summary>
    public class BlockDto
    {
        /// <summary>
        /// Id blokiranja
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Korisnik koji blokira drugog korisnika
        /// </summary>
        public int BlockerId { get; set; }

        /// <summary>
        /// Korisnik koji je blokiran
        /// </summary>
        public int BlockedId { get; set; }

        /// <summary>
        /// Vreme blokiranja
        /// </summary>
        public DateTime? BlockDate { get; set; }
    }
}
