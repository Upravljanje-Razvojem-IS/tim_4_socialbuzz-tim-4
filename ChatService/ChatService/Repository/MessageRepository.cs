using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChatService.CustomException;
using ChatService.Database;
using ChatService.DTOs;
using ChatService.Entities;
using ChatService.Interfaces;
using ChatService.Models;
using Logistics.API.MockLogger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatService.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly FakeLogger _logger;

        public MessageRepository(DatabaseContext context, IMapper mapper, FakeLogger logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public MessageConfirmationDto Create(MessageCreateDto dto)
        {
            Message newEntity = new Message()
            {
                Id = Guid.NewGuid(),
                Content = dto.Content,
                Created = DateTime.UtcNow,
                ReciverId = dto.ReciverId,
                SenderId = dto.SenderId
            };

            _context.Messages.Add(newEntity);
            _context.SaveChanges();

            _logger.Log("Create Message");

            return _mapper.Map<MessageConfirmationDto>(newEntity);
        }

        public void Delete(Guid id)
        {
            var entity = _context.Messages.FirstOrDefault(e => e.Id == id);

            if (entity == null)
                throw new BussinessException("Message does not exist");

            _context.Remove(entity);
            _context.SaveChanges();

            _logger.Log("Delete Message");
        }

        public List<MessageReadDto> Get()
        {
            var entities = _context.Messages
                .ToList();

            _logger.Log("Get Messages");

            return _mapper.Map<List<MessageReadDto>>(entities);
        }

        public MessageReadDto Get(Guid id)
        {
            var entity = _context.Messages
                .FirstOrDefault(e => e.Id == id);

            _logger.Log("Get Message");

            return _mapper.Map<MessageReadDto>(entity);
        }

        public MessageConfirmationDto Update(Guid id, MessageCreateDto dto)
        {
            var entity = _context.Messages.FirstOrDefault(e => e.Id == id);

            entity.Content = dto.Content;
            entity.Created = dto.Created;
            entity.ReciverId = dto.ReciverId;
            entity.SenderId = dto.SenderId;

            _context.SaveChanges();

            _logger.Log("Update Message");

            return _mapper.Map<MessageConfirmationDto>(entity);
        }

        public List<MessageReadDto> GetByReciverAndSender(SenderAndReciver model)
        {
            var list = _context.Messages.Where(e => e.SenderId == model.Sender && e.ReciverId == model.Reciver);

            _logger.Log("Get Messages by sender and reciver");

            return _mapper.Map<List<MessageReadDto>>(list);
        }

        public List<MessageReadDto> SearchMessagesByContent(string content)
        {
            var list = _context.Messages.Where(e => e.Content.Contains(content));

            _logger.Log("Search messages by content");

            return _mapper.Map<List<MessageReadDto>>(list);
        }
    }
}
