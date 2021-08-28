using ChatService.DTOs;
using ChatService.Models;
using System;
using System.Collections.Generic;

namespace ChatService.Interfaces
{
    public interface IMessageRepository
    {
        List<MessageReadDto> Get();
        MessageReadDto Get(Guid id);
        MessageConfirmationDto Create(MessageCreateDto dto);
        MessageConfirmationDto Update(Guid id, MessageCreateDto dto);
        void Delete(Guid id);
        List<MessageReadDto> GetByReciverAndSender(SenderAndReciver model);
        List<MessageReadDto> SearchMessagesByContent(string content);
    }
}
