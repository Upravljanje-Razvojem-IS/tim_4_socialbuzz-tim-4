﻿using GroupChatService.Database;
using GroupChatService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GroupChatService.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
<<<<<<< HEAD
        private AppDbContext _ctx;
=======
        private readonly AppDbContext _ctx;
>>>>>>> it61g2017
        public RoomViewComponent(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var chats = _ctx.ChatUsers
                .Include(x=>x.Chat)
                .Where(x=>x.UserId == userId 
                    && x.Chat.Type == ChatType.Room)
                .Select(x=>x.Chat)
                .ToList();
            
            return View(chats);
        }
    }
}
