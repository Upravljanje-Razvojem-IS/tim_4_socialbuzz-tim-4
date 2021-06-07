using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Models.Mocks
{
    /// <summary>
    /// DTO za model objava korisnika
    /// </summary>
    public class PostDto
    {
        /// <summary>
        /// ID objave
        /// </summary>
        public int PostID { get; set; }

        /// <summary>
        /// ID proizvoda koji korisnik prodaje putem svoje objave
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Naziv proizvoda koji korisnik prodaje putem svoje objave
        /// </summary>
        public String ProductName { get; set; }

        /// <summary>
        /// ID korisnika koji je okacio objavu
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Opis proizvoda koji se prodaje
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Cena proizvoda
        /// </summary>
        public String Price { get; set; }

        /// <summary>
        /// Dostupna kolicina proizvoda 
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Datum kada je korisnik postavio objavu
        /// </summary>
        public DateTime PostedOn { get; set; }
    }
}
