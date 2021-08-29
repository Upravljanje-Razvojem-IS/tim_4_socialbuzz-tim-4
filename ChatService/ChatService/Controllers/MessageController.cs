using ChatService.DTOs;
using ChatService.Interfaces;
using ChatService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ChatService.Controllers
{
    [ApiController]
    [Route("api/message")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _repository;

        public MessageController(IMessageRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all messages
        /// </summary>
        /// <returns>list of messages</returns>
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
        /// Get message by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Message</returns>
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
        /// Create new message
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>new message</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult PostMessage([FromBody] MessageCreateDto dto)
        {
            var entity = _repository.Create(dto);

            return Ok(entity);
        }

        /// <summary>
        /// Update existing message
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>updated message</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult PutMessage(Guid id, MessageCreateDto dto)
        {
            var entity = _repository.Update(id, dto);

            return Ok(entity);
        }

        /// <summary>
        /// Delete message by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete]
        public ActionResult DeleteMessage(Guid id)
        {
            _repository.Delete(id);

            return NoContent();
        }


        /// <summary>
        /// Get all messages by sender and reciver
        /// </summary>
        /// <returns>list of messages</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("reciver-sender")]
        public ActionResult GetByReciverAndSender([FromBody] SenderAndReciver model)
        {
            var entities = _repository.GetByReciverAndSender(model);

            if (entities.Count == 0)
                return NoContent();

            return Ok(entities);
        }

        /// <summary>
        /// Search messages by content
        /// </summary>
        /// <returns>list of users</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("search")]
        public ActionResult SearchMessagesByContent([FromBody] string content)
        {
            var entities = _repository.SearchMessagesByContent(content);

            if (entities.Count == 0)
                return NoContent();

            return Ok(entities);
        }

    }
}
