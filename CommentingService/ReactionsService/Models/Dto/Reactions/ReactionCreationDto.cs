using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Models.Dto
{
    /// <summary>
    /// DTO za dodavanje nove reakcije na objavu
    /// </summary>
    public class ReactionCreationDto
    {
        /// <summary>
        /// ID objave na koju se dodaje reakcija
        /// </summary>
        public int PostID { get; set; }

        /// <summary>
        /// ID tipa reakcije
        /// </summary>
        [Required(ErrorMessage = "Type of reaction is required!")]
        public int TypeOfReactionID { get; set; }
    }
}
