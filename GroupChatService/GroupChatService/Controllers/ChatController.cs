using GroupChatService.Database;
using GroupChatService.Hubs;
using GroupChatService.Infrastructure;
using GroupChatService.Infrastructure.Repository;
using GroupChatService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChatService.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : BaseController
    {
        
        private IHubContext<ChatHub> _chat;

        public ChatController(
            IHubContext<ChatHub> chat)
        {
            _chat = chat;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(
            int roomId,
            string message,
            [FromServices] IChatRepository repo)
        {
            var Message = await repo.CreateMessage(roomId, message, User.Identity.Name);

            await _chat.Clients.Group(roomId.ToString())
                .SendAsync("RecieveMessage", new
                {
                    Text = Message.Text,
                    Name = Message.Name,
                    Timestamp = Message.Timestamp.ToString("dd/MM/yyyy hh:mm:ss")
                });

            return Ok();
        }
    }
}
