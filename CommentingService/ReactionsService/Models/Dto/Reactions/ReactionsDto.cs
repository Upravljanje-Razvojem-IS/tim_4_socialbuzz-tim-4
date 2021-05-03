using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Models.Dto
{
    /// <summary>
    /// DTO za model reakcije
    /// </summary>
    public class ReactionsDto
    {
        /// <summary>
        /// ID reakcije
        /// </summary>
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
