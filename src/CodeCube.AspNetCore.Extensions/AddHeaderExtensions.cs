using System;
using Microsoft.AspNetCore.Builder;

namespace CodeCube.AspNetCore.Extensions
{
    public static class AddHeaderExtensions
    {
        public static IApplicationBuilder UseVersionHeader(this IApplicationBuilder builder, Action<VersionHeaderOptions> setupAction = null)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var options = new VersionHeaderOptions();
            setupAction?.Invoke(options);

            return builder.UseMiddleware<AddHeadersMiddleware>(options);
        }
    }
}
