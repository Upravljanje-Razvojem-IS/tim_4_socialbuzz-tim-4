using System;
using System.ComponentModel.DataAnnotations;

namespace QualityRanking.DTOs
{
    public class RankingPostDTO
    {
        [Range(1, 10)]
        public int Rate { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
    }
}
