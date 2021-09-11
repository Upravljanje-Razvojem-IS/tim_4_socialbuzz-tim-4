using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models.ListingType
{
    /// <summary>
    /// DTO model za izmenu tipa listinga
    /// </summary>
    public class ListingTypeUpdateDto
    {
        #region Properties

        /// <summary>
        /// ID tipa listinga
        /// </summary>
        [Required(ErrorMessage = "ListingTypeId is required.")]
        public int ListingTypeId { get; set; }

        /// <summary>
        /// Naziv tipa listinga
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        #endregion
    }
}
