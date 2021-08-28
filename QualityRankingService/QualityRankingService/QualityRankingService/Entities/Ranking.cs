using System;
using System.ComponentModel.DataAnnotations;

namespace QualityRanking.Entities
{
    public class Ranking
    {
        /// <summary>
        /// Primary key GUID
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
        [Required]
        public int Rate { get; set; }
        /// <summary>
        /// Ranking description
        /// </summary>
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// ID of rated user
        /// </summary>
        [Required]
        public Guid RaterId { get; set; }
        [Required]
        public Guid RateeId { get; set; }
    }
}
