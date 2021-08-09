using QualityRanking.DTOs;
using System;
using System.Collections.Generic;

namespace QualityRanking.Interfaces
{
    public interface IRankingRepository
    {
        List<RankingGetDTO> Get();
        RankingGetDTO Get(Guid id);
        RankingConfirmDTO Create(RankingPostDTO dto);
        RankingConfirmDTO Update(Guid id, RankingPostDTO dto);
        void Delete(Guid id);
    }
}
