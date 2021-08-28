using System;
using System.ComponentModel.DataAnnotations;

namespace QualityRanking.DTOs
{
    public class RankingPostDto
    {
        /// <summary>
        /// Rate from 1 to 10
        /// </summary>
        [Range(1, 10)]
        public int Rate { get; set; }
        /// <summary>
        /// Rnking description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// ID of rated user
        /// </summary>
        public Guid RaterId { get; set; }
        public Guid RateeId { get; set; }
    }
}
