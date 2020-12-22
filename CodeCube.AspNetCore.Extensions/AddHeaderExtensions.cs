using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

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

    public class AddHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly VersionHeaderOptions _options;

        //The middleware class must include:
        //1. A public constructor with a parameter of type RequestDelegate.
        public AddHeadersMiddleware(RequestDelegate next, VersionHeaderOptions options)
        {
            _next = next;
            _options = options ?? new VersionHeaderOptions();
        }

        //2. A public method named Invoke or InvokeAsync.This method must:
        //   - Return a Task.
        //   - Accept a first parameter of type HttpContext.
        public async Task InvokeAsync(HttpContext context)
        {
            var versionForHeader = GetVersion(_options.Version, _options.Assembly);

            context.Response.OnStarting((state) =>
            {
                context.Response.Headers.Add(_options.HeaderName, versionForHeader);

                return Task.FromResult(0);
            }, context);

            await _next.Invoke(context);
        }

        private static string GetVersion(string optionsVersion, string optionsAssembly)
        {
            if (!string.IsNullOrWhiteSpace(optionsVersion)) return optionsVersion;

            var theAssembly = !string.IsNullOrWhiteSpace(optionsAssembly) ? Assembly.Load(optionsAssembly) : Assembly.GetEntryAssembly();
            var assemblyVersionInformation = theAssembly?.GetCustomAttribute<AssemblyFileVersionAttribute>();

            if (assemblyVersionInformation != null)
            {
                return assemblyVersionInformation.Version;
            }

            return "0.0.0.0";
        }
    }
}
