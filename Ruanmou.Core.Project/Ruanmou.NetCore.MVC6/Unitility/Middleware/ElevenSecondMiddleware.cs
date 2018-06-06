using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruanmou.NetCore.MVC6.Unitility.Middleware
{
    public class ElevenSecondMiddleware
    {
        private readonly RequestDelegate _next;

        public ElevenSecondMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"{nameof(ElevenSecondMiddleware)}Eleven Hello World Too1!<br/>");
            await _next(context);
            await context.Response.WriteAsync($"{nameof(ElevenSecondMiddleware)}Eleven Hello World Too2!<br/>");
        }
    }
}
