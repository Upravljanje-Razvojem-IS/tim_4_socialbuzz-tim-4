using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Models.Dto
{
    /// <summary>
    /// DTO za model tipa reakcije
    /// </summary>
    public class TypeOfReactionDto
    {
        /// <summary>
        /// ID tipa reakcije
        /// </summary>
        public int TypeOfReactionID { get; set; }

        /// <summary>
        /// Naziv tipa reakcije
        /// </summary>
        public String ReactionType { get; set; }
    }
}
