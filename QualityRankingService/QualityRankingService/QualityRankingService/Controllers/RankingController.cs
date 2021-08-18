using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualityRanking.DTOs;
using QualityRanking.Interfaces;
using System;

namespace QualityRanking.Controllers
{
    [ApiController]
    [Route("api/rank")]
    [Authorize]
    public class RankingController : ControllerBase
    {
        private readonly IRankingRepository _repository;

        public RankingController(IRankingRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all Rankings
        /// </summary>
        /// <returns>List of all rankings</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public ActionResult GetAll()
        {
            var entities = _repository.Get();

            if (entities.Count == 0)
                return NoContent();

            return Ok(entities);
        }

        /// <summary>
        /// Return rank by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Rank</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var entity = _repository.Get(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        /// <summary>
        /// Post new rank
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>New rank</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult PostRank([FromBody] RankingPostDto dto)
        {
            var entity = _repository.Create(dto);

            if (entity == null)
                return BadRequest();

            return Ok(entity);
        }

        /// <summary>
        /// Update rank
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>updated rank</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult PutRank(Guid id, RankingPostDto dto)
        {
            var entity = _repository.Update(id, dto);

            if (entity == null)
                return BadRequest();

            return Ok(entity);
        }

        /// <summary>
        /// Delete rank by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete]
        public ActionResult DeleteRank(Guid id)
        {
            _repository.Delete(id);

            return NoContent();
        }
    }
}
