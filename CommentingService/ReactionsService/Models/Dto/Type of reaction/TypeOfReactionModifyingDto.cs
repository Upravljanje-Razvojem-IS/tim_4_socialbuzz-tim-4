using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Models.Dto
{
    /// <summary>
    /// DTO za modifikaciju tipa reakcije
    /// </summary>
    public class TypeOfReactionModifyingDto
    {
        /// <summary>
        /// ID tipa rekacije koji se modifikuje
        /// </summary>
        public int TypeOfReactionID { get; set; }

        /// <summary>
        /// Naziv tipa reakcije koji se modifikuje
        /// </summary>
        public String ReactionType { get; set; }
    }
}
