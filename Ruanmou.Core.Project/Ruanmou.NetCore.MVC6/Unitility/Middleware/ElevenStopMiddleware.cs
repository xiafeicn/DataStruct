using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruanmou.NetCore.MVC6.Unitility.Middleware
{
    /// <summary>
    /// app.UseMiddleware<ElevenMiddleware>();
    /// </summary>
    public class ElevenStopMiddleware
    {
        private readonly RequestDelegate _next;

        public ElevenStopMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.Contains("Eleven"))
                await context.Response.WriteAsync($"{nameof(ElevenStopMiddleware)}这里是Eleven的终结点<br/>");
            else
            {
                await context.Response.WriteAsync($"{nameof(ElevenStopMiddleware)}Eleven,Hello World2!<br/>");
                await _next(context);
                await context.Response.WriteAsync($"{nameof(ElevenStopMiddleware)}Eleven,Hello World2!<br/>");
            }
        }
    }
}
