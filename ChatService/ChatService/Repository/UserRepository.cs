using AutoMapper;
using ChatService.CustomException;
using ChatService.Database;
using ChatService.DTOs;
using ChatService.Entities;
using ChatService.Interfaces;
using Logistics.API.MockLogger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly FakeLogger _logger;

        public UserRepository(FakeLogger logger, IMapper mapper, DatabaseContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public UserConfirmationDto Create(UserCreateDto dto)
        {
            User newEntity = new User()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                LastName = dto.LastName,
                Password = dto.Password
            };

            _context.Users.Add(newEntity);
            _context.SaveChanges();

            _logger.Log("Create Message");

            return _mapper.Map<UserConfirmationDto>(newEntity);
        }

        public void Delete(Guid id)
        {
            var entity = _context.Users.FirstOrDefault(e => e.Id == id);

            if (entity == null)
                throw new BussinessException("Message does not exist");

            _context.Remove(entity);
            _context.SaveChanges();

            _logger.Log("Delete Message");
        }

        public List<UserReadDto> Get()
        {
            var entities = _context.Users
                .ToList();

            _logger.Log("Get Messages");

            return _mapper.Map<List<UserReadDto>>(entities);
        }

        public UserReadDto Get(Guid id)
        {
            var entity = _context.Users
                .FirstOrDefault(e => e.Id == id);

            _logger.Log("Get Message");

            return _mapper.Map<UserReadDto>(entity);
        }

        public UserConfirmationDto Update(Guid id, UserCreateDto dto)
        {
            var entity = _context.Users.FirstOrDefault(e => e.Id == id);

            entity.Name = dto.Name;
            entity.Email = dto.Email;
            entity.LastName = dto.LastName;
            entity.Password = dto.Password;

            _context.SaveChanges();

            _logger.Log("Update Message");

            return _mapper.Map<UserConfirmationDto>(entity);
        }
    }
}
