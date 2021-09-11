using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models.Listing
{
    /// <summary>
    /// DTO model izmene listinga
    /// </summary>
    public class ListingUpdateDto : IValidatableObject
    {
        #region Properties

        /// <summary>
        /// ID listinga
        /// </summary>
        [Required(ErrorMessage = "ListingId is required.")]
        public Guid ListingId { get; set; }

        /// <summary>
        /// Naziv listinga
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        /// <summary>
        /// Opis listinga
        /// </summary>
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        /// <summary>
        /// Cena u listingu
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Kontaktirati za cenu bit
        /// </summary>
        public bool PriceContact { get; set; }

        /// <summary>
        /// Dogovor za cenu bit
        /// </summary>
        public bool PriceDeal { get; set; }

        #endregion

        #region Foreign keys

        /// <summary>
        /// ID kategorije
        /// </summary>
        [Required(ErrorMessage = "CategoryId is required.")]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// ID tipa listinga
        /// </summary>
        [Required(ErrorMessage = "ListingTypeId is required.")]
        public int ListingTypeId { get; set; }

        #endregion

        #region Mock properties/foreign keys

        /// <summary>
        /// ID korisnika
        /// </summary>
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        #endregion

        /// <summary>
        /// Validacija unetih vrednosti
        /// </summary>
        /// <remarks>
        /// Ako je cena manja od 0 ili nije zadata i bit za kontakt je false i bit za dogovor je false daje grešku u validaciji
        /// Ako je ID kategorije prazan ili nov Guid daje grešku u validaciji
        /// Ako je ID tipa listinga manji od 1 daje grešku u validaciji
        /// Ako je ID korisnika manji od 1 daje grešku u validaciji
        /// </remarks>
        /// <returns>Rezultat validacije</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Price <= 0 && PriceContact == false && PriceDeal == false)
                yield return new ValidationResult(
                    "You have to set the price on listing, or choose the option to contact or make a deal.",
                    new[] { "ListingUpdateDto" });

            if (CategoryId == new Guid())
                yield return new ValidationResult(
                    "CategoryId is invalid.",
                    new[] { "ListingUpdateDto" });

            if (ListingTypeId < 1)
                yield return new ValidationResult(
                    "ListingTypeId is invalid.",
                    new[] { "ListingUpdateDto" });

            if (UserId < 1)
                yield return new ValidationResult(
                    "UserId is invalid.",
                    new[] { "ListingUpdateDto" });
        }
    }
}
