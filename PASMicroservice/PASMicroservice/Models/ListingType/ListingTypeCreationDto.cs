using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models.ListingType
{
    /// <summary>
    /// DTO model za kreiranje tipa listinga
    /// </summary>
    public class ListingTypeCreationDto
    {
        #region Properties

        /// <summary>
        /// Naziv tipa listinga
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        #endregion
    }
}
