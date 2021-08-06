using System;
using AspNetCore.RapiDoc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder
{
    public static class RapiDocBuilderExtensions
    {
        /// <summary>
        /// Register the RapiDoc middleware with provided options
        /// </summary>
        public static IApplicationBuilder UseRapiDoc(this IApplicationBuilder app, RapiDocOptions options)
        {
            return app.UseMiddleware<RapiDocMiddleware>(options);
        }

        /// <summary>
        /// Register the RapiDoc middleware with optional setup action for DI-injected options
        /// </summary>
        public static IApplicationBuilder UseRapiDoc(
            this IApplicationBuilder app,
            Action<RapiDocOptions> setupAction = null)
        {
            RapiDocOptions options;
            using (var scope = app.ApplicationServices.CreateScope())
            {
                options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<RapiDocOptions>>().Value;
                setupAction?.Invoke(options);
            }

            // To simplify the common case, use a default that will work with the RapiDocMiddleware defaults
            if (string.IsNullOrWhiteSpace(options.SpecUrl))
            {
                options.SpecUrl = "/swagger/v1/swagger.json";
            }

            return app.UseRapiDoc(options);
        }
    }
}
