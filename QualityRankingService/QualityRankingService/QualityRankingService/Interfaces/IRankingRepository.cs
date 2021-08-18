using QualityRanking.DTOs;
using System;
using System.Collections.Generic;

namespace QualityRanking.Interfaces
{
    public interface IRankingRepository
    {
        List<RankingGetDto> Get();
        RankingGetDto Get(Guid id);
        RankingConfirmDto Create(RankingPostDto dto);
        RankingConfirmDto Update(Guid id, RankingPostDto dto);
        void Delete(Guid id);
    }
}
