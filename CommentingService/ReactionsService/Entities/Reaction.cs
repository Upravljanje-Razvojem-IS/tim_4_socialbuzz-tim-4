using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Models
{
    // <summary>
    /// Model reakcije na objavu
    /// </summary>
    [Table("Reactions")]
    public class Reaction
    {
        /// <summary>
        /// ID reakcije
        /// </summary>
        [Key]
        public Guid ReactionID { get; set; }

        /// <summary>
        /// ID objave na koju se dodaje reakcija
        /// </summary>
        public int PostID { get; set; }

        /// <summary>
        /// ID tipa reakcije
        /// </summary>
        public int TypeOfReactionID { get; set; }

        /// <summary>
        /// ID korisnika koji dodaje reakciju
        /// </summary>
        public int UserID { get; set; }
    }
}

