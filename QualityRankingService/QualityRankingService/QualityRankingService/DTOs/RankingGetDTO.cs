using System;

namespace QualityRanking.DTOs
{
    public class RankingGetDto
    {
        public Guid Id { get; set; }
        public int Rate { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
    }
}
