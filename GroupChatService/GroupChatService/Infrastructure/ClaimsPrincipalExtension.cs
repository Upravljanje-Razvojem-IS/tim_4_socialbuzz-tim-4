﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GroupChatService.Infrastructure
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUserId(this ClaimsPrincipal @this)
        {
            return @this.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
