using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Models.Dto
{
    /// <summary>
    /// DTO za dodavanje novog tipa reakcije na objavu
    /// </summary>
    public class TypeOfReactionCreationDto
    {
        /// <summary>
        /// Naziv tipa reakcije koja se kreira
        /// </summary>
        [Required(ErrorMessage = "Reaction type is required!")]
        public String ReactionType { get; set; }

    }
}
