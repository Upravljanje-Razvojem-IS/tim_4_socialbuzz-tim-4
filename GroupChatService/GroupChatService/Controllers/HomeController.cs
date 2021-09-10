using GroupChatService.Database;
using GroupChatService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GroupChatService.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private AppDbContext _ctx;
        public HomeController(AppDbContext ctx) => _ctx = ctx;

        public IActionResult Index()
        {
            var chats = _ctx.Chats
                .Include(x=>x.Users)
                .Where(x => !x.Users
                    .Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
                .ToList();
            return View(chats);
        }

        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            var chat = _ctx.Chats
                .Include(x =>x.Messages)
                .FirstOrDefault(x => x.Id == id);
            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId, string message)
        {
            var Message = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = User.Identity.Name,
                Timestamp = DateTime.Now
            };

            _ctx.Messages.Add(Message);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Chat", new { id = chatId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Room
            };

            chat.Users.Add(new ChatUser
            {
                UserId=User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Owner
            });

            _ctx.Chats.Add(chat);

            await _ctx.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> JoinRoom(int id)
        {
            var chatUser = new ChatUser
            {
                ChatId=id,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Member
            };

            _ctx.ChatUsers.Add(chatUser);

            await _ctx.SaveChangesAsync();

            return RedirectToAction("Chat","Home",new { id = id });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(int chatId)
        {
            var room = _ctx.Chats.SingleOrDefault(c => c.Id == chatId);
            _ctx.Chats.Remove(room);

            await _ctx.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoom(Chat chat)
        {
            _ctx.Chats.Update(chat);

            await _ctx.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
