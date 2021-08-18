using System;
using System.ComponentModel.DataAnnotations;

namespace QualityRanking.DTOs
{
    public class RankingPostDto
    {
        [Range(1, 10)]
        public int Rate { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
    }
}
