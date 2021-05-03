using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Models.Dto
{
    /// <summary>
    /// DTO za modifikaciju reakcije
    /// </summary>
    public class ReactionModifyingDto
    {
        /// <summary>
        /// ID reakcije koja se modifikuje
        /// </summary>
        public Guid ReactionID { get; set; }

        /// <summary>
        /// ID objave na kojoj se nalazi reakcija koja se modifikuje
        /// </summary>
        public int PostID { get; set; }

        /// <summary>
        /// ID tipa reakcije kojoj pripada reakcija koja se modifikuje
        /// </summary>
        public int TypeOfReactionID { get; set; }
    }
}
