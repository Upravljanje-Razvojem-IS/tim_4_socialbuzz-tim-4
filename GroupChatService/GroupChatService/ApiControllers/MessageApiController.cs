using GroupChatService.Hubs;
using GroupChatService.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroupChatService.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageApiController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _chat;

        public MessageApiController(
            IHubContext<ChatHub> chat)
        {
            _chat = chat;
        }

        [HttpPost("/sendMessage")]
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
