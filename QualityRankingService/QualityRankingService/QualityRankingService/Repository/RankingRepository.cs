using AutoMapper;
using QualityRanking.Database;
using QualityRanking.DTOs;
using QualityRanking.Entities;
using QualityRanking.Interfaces;
using QualityRanking.Mock;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QualityRanking.Repository
{
    public class RankingRepository : IRankingRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly Logger _logger;

        public RankingRepository(IMapper mapper, DatabaseContext context, Logger logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public RankingConfirmDTO Create(RankingPostDTO dto)
        {
            User user = MockUser.Users.FirstOrDefault(e => e.Id == dto.UserId);

            if (user == null)
                return null;

            Ranking newRanking = new Ranking()
            {
                Id = Guid.NewGuid(),
                Rate = dto.Rate,
                Description = dto.Description,
                UserId = dto.UserId
            };

            _context.Rankings.Add(newRanking);
            _context.SaveChanges();

            _logger.Log("New ranking has been created");

            return _mapper.Map<RankingConfirmDTO>(newRanking);
        }

        public List<RankingGetDTO> Get()
        {
            var list = _context.Rankings.ToList();


            _logger.Log("Fethc list of all rankings");

            return _mapper.Map<List<RankingGetDTO>>(list);
        }

        public RankingGetDTO Get(Guid id)
        {
            var rank = _context.Rankings.FirstOrDefault(e => e.Id == id);


            _logger.Log("Fetch ranking by given Id");

            return _mapper.Map<RankingGetDTO>(rank);
        }

        public RankingConfirmDTO Update(Guid id, RankingPostDTO dto)
        {
            var rank = _context.Rankings.FirstOrDefault(e => e.Id == id);

            if (rank == null)
                return null;

            User user = MockUser.Users.FirstOrDefault(e => e.Id == dto.UserId);

            if (user == null)
                return null;

            rank.Rate = dto.Rate;
            rank.Description = dto.Description;
            rank.UserId = dto.UserId;

            _context.SaveChanges();

            _logger.Log("Ranking has been updated");

            return _mapper.Map<RankingConfirmDTO>(rank);
        }

        public void Delete(Guid id)
        {
            var rank = _context.Rankings.FirstOrDefault(e => e.Id == id);

            if (rank != null)
            {
                _context.Rankings.Remove(rank);
                _context.SaveChanges();


                _logger.Log("Ranking has been deleted");
            }
        }

    }
}
