using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Models
{
    /// <summary>
    /// Model tipa reakcije na objavu
    /// </summary>
    [Table("TypeOfReaction")]
    public class TypeOfReaction
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
