using System.Reflection;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.StaticFiles;
using System.Linq;
using AspNetCore.RapiDoc.Util;

#if NETSTANDARD2_0
using IWebHostEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
#endif

namespace AspNetCore.RapiDoc
{
    public class RapiDocMiddleware
    {
        private const string EmbeddedFileNamespace = "AspNetCore.RapiDoc.content";

        private readonly RapiDocOptions _options;
        private readonly StaticFileMiddleware _staticFileMiddleware;

        public RapiDocMiddleware(
            RequestDelegate next,
            IWebHostEnvironment hostingEnv,
            ILoggerFactory loggerFactory,
            RapiDocOptions options)
        {
            _options = options ?? new RapiDocOptions();

            _staticFileMiddleware = CreateStaticFileMiddleware(next, hostingEnv, loggerFactory, options);
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var httpMethod = httpContext.Request.Method;
            var path = httpContext.Request.Path.Value;

            // If the RoutePrefix is requested (with or without trailing slash), redirect to index URL
            if (httpMethod == "GET" && Regex.IsMatch(path, $"^/?{Regex.Escape(_options.RoutePrefix)}/?$", RegexOptions.IgnoreCase))
            {
                // Use relative redirect to support proxy environments
                var relativeIndexUrl = string.IsNullOrEmpty(path) || path.EndsWith("/")
                    ? "index.html"
                    : $"{path.Split('/').Last()}/index.html";

                RespondWithRedirect(httpContext.Response, relativeIndexUrl);
                return;
            }

            if (httpMethod == "GET" && Regex.IsMatch(path, $"^/{Regex.Escape(_options.RoutePrefix)}/?index.html$", RegexOptions.IgnoreCase))
            {
                await RespondWithIndexHtml(httpContext.Response);
                return;
            }

            await _staticFileMiddleware.Invoke(httpContext);
        }

        private StaticFileMiddleware CreateStaticFileMiddleware(
            RequestDelegate next,
            IWebHostEnvironment hostingEnv,
            ILoggerFactory loggerFactory,
            RapiDocOptions options)
        {
            var staticFileOptions = new StaticFileOptions
            {
                RequestPath = string.IsNullOrEmpty(options.RoutePrefix) ? string.Empty : $"/{options.RoutePrefix}",
                FileProvider = new EmbeddedFileProvider(typeof(RapiDocMiddleware).GetTypeInfo().Assembly, EmbeddedFileNamespace),
            };

            return new StaticFileMiddleware(next, hostingEnv, Options.Create(staticFileOptions), loggerFactory);
        }

        private void RespondWithRedirect(HttpResponse response, string location)
        {
            response.StatusCode = 301;
            response.Headers["Location"] = location;
        }

        private async Task RespondWithIndexHtml(HttpResponse response)
        {
            response.StatusCode = 200;
            response.ContentType = "text/html;charset=utf-8";

            using (var stream = _options.IndexStream())
            {
                // Inject arguments before writing to response
                var htmlBuilder = new StringBuilder(new StreamReader(stream).ReadToEnd());
                foreach (var entry in GetIndexArguments())
                {
                    htmlBuilder.Replace(entry.Key, entry.Value);
                }

                await response.WriteAsync(htmlBuilder.ToString(), Encoding.UTF8);
            }
        }

        private IDictionary<string, string> GetIndexArguments()
        {
            return new Dictionary<string, string>()
            {
                //General Options
                { "%(HeadingText)", _options.HeadingText },
                { "%(DocumentTitle)", _options.HeadingText },
                { "%(HeadContent)", _options.HeadContent },
                { "%(SpecUrl)", _options.SpecUrl },
                { "%(SortTags)", _options.SortTags.ToString().ToLower() },
                { "%(SortEndpointsBy)", _options.SortEndpointsBy.GetDisplayValue() },
                { "%(GotoPath)", _options.GotoPath?.ToString()  ?? string.Empty},
                { "%(FillRequestFieldsWithExample)", _options.FillRequestFieldsWithExample.ToString().ToLower() },

                //UI Colors And Fonts Options
                { "%(Theme)", _options.UIColorsAndFontsOptions.Theme.GetDisplayValue() },
                { "%(BgColor)", _options.UIColorsAndFontsOptions.BgColor.ToHtml() },
                { "%(TextColor)", _options.UIColorsAndFontsOptions.TextColor.ToHtml() },
                { "%(HeaderColor)", _options.UIColorsAndFontsOptions.HeaderColor.ToHtml() },
                { "%(PrimaryColor)", _options.UIColorsAndFontsOptions.PrimaryColor.ToHtml() },
                { "%(LoadFonts)", _options.UIColorsAndFontsOptions.LoadFonts.ToString().ToLower() },
                { "%(RegularFont)", _options.UIColorsAndFontsOptions.RegularFont },
                { "%(MonoFont)", _options.UIColorsAndFontsOptions.MonoFont },
                { "%(FontSize)", _options.UIColorsAndFontsOptions.FontSize.GetDisplayValue() },

                //Navigation bar settings
                { "%(UsePathInNavBar)", _options.NavigationBarOptions.UsePathInNavBar.ToString().ToLower() },
                { "%(NavBgColor)", _options.NavigationBarOptions.NavBgColor.ToHtml() },
                { "%(NavBgImage)", _options.NavigationBarOptions.NavBgImage },
                { "%(NavBgImageSize)", _options.NavigationBarOptions.NavBgImageSize.GetDisplayValue() },
                { "%(NavBgImageRepeat)", _options.NavigationBarOptions.NavBgImageRepeat.GetDisplayValue() },
                { "%(NavTextColor)", _options.NavigationBarOptions.NavTextColor.ToHtml() },
                { "%(NavHoverBgColor)", _options.NavigationBarOptions.NavHoverBgColor.ToHtml() },
                { "%(NavHoverTextColor)", _options.NavigationBarOptions.NavHoverTextColor.ToHtml() },
                { "%(NavAccentColor)", _options.NavigationBarOptions.NavAccentColor.ToHtml() },
                { "%(NavItemSpacing)", _options.NavigationBarOptions.NavItemSpacing.GetDisplayValue() },

                //UI Layout & Placement
                { "%(Layout)", _options.UILayoutOptions.Layout.GetDisplayValue() },
                { "%(RenderStyle)", _options.UILayoutOptions.RenderStyle.GetDisplayValue() },
                { "%(OnNavTagClick)", _options.UILayoutOptions.OnNavTagClick.GetDisplayValue() },
                { "%(SchemaStyle)", _options.UILayoutOptions.SchemaStyle.GetDisplayValue() },
                { "%(SchemaExpandLevel)", _options.UILayoutOptions.SchemaExpandLevel.ToString() },
                { "%(SchemaDescriptionExpanded)", _options.UILayoutOptions.SchemaDescriptionExpanded.ToString().ToLower() },
                { "%(SchemaHideReadOnly)", _options.UILayoutOptions.SchemaHideReadOnly.GetDisplayValue() },
                { "%(SchemaHideWriteOnly)", _options.UILayoutOptions.SchemaHideWriteOnly.GetDisplayValue() },
                { "%(DefaultSchemaTab)", _options.UILayoutOptions.DefaultSchemaTab.GetDisplayValue() },
                { "%(ResponseAreaHeight)", _options.UILayoutOptions.ResponseAreaHeight },

                //Navigation bar settings
                { "%(ShowInfo)", _options.SectionOptions.ShowInfo.ToString().ToLower() },
                { "%(InfoDescriptionHeadingsInNavbar)", _options.SectionOptions.InfoDescriptionHeadingsInNavbar.ToString().ToLower() },
                { "%(ShowComponents)", _options.SectionOptions.ShowComponents.ToString().ToLower() },
                { "%(ShowHeader)", _options.SectionOptions.ShowHeader.ToString().ToLower() },
                { "%(AllowAuthentication)", _options.SectionOptions.AllowAuthentication.ToString().ToLower() },
                { "%(AllowSpecUrlLoad)", _options.SectionOptions.AllowSpecUrlLoad.ToString().ToLower() },
                { "%(AllowSpecFileLoad)", _options.SectionOptions.AllowSpecFileLoad.ToString().ToLower() },
                { "%(AllowSearch)", _options.SectionOptions.AllowSearch.ToString().ToLower() },
                { "%(AllowAdvancedSearch)", _options.SectionOptions.AllowAdvancedSearch.ToString().ToLower() },
                { "%(AllowTry)", _options.SectionOptions.AllowTry.ToString().ToLower() },
                { "%(AllowServerSelection)", _options.SectionOptions.AllowServerSelection.ToString().ToLower() },
                { "%(AllowSchemaDescriptionExpandToggle)", _options.SectionOptions.AllowSchemaDescriptionExpandToggle.ToString().ToLower() },

                //Server Api
                { "%(ServerUrl)", _options.ApiServerOptions.ServerUrl },
                { "%(DefaultApiServer)", _options.ApiServerOptions.DefaultApiServer },
                { "%(ApiKeyName)", _options.ApiServerOptions.ApiKeyName },
                { "%(ApiKeyLocation)", _options.ApiServerOptions.ApiKeyLocation.GetDisplayValue() },
                { "%(ApiKeyValue)", _options.ApiServerOptions.ApiKeyValue },
                { "%(FetchCredentials)", _options.ApiServerOptions.FetchCredentials.GetDisplayValue() },
            };
        }
    }
}