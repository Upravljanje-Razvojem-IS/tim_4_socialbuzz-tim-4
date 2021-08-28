using System;
namespace QualityRanking.DTOs
{
    public class RankingGetDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
        public int Rate { get; set; }
        /// <summary>
        /// Ranking description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// ID of rated user
        /// </summary>
        public Guid RaterId { get; set; }
        public Guid RateeId { get; set; }
    }
}
