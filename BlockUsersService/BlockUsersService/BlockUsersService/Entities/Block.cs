using System;
using System.Collections.Generic;

#nullable disable

namespace BlockUsersService.Entities
{
    /// <summary>
    /// Model tabele u bazi blokiranja korisnika
    /// </summary>
    public partial class Block
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
        /// Blokirani korisnik
        /// </summary>
        public int BlockedId { get; set; }

        /// <summary>
        /// Vreme trenutka blokiranja
        /// </summary>
        public DateTime? BlockDate { get; set; }
    }
}
