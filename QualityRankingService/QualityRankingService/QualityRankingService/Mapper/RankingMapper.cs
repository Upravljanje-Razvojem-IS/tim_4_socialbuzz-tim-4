using AutoMapper;
using QualityRanking.DTOs;
using QualityRanking.Entities;

namespace QualityRanking.Mapper
{
    public class RankingMapper : Profile
    {
        public RankingMapper()
        {
            CreateMap<Ranking, RankingGetDto>();
            CreateMap<Ranking, RankingConfirmDto>();
        }
    }
}
