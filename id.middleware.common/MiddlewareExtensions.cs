using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace id.middleware.common
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseProcessingTimeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ProcessingTimeMiddleware>();
        }
    }
}
