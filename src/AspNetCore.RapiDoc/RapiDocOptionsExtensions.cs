using System.Text;
using AspNetCore.RapiDoc;

namespace Microsoft.AspNetCore.Builder
{
    public static class RapiDocOptionsExtensions
    {
        /// <summary>
        /// Injects additional CSS stylesheets into the index.html page
        /// </summary>
        /// <param name="options"></param>
        /// <param name="path">A path to the stylesheet - i.e. the link "href" attribute</param>
        /// <param name="media">The target media - i.e. the link "media" attribute</param>
        public static void InjectStylesheet(this RapiDocOptions options, string path, string media = "screen")
        {
            var builder = new StringBuilder(options.HeadContent);
            builder.AppendLine($"<link href='{path}' rel='stylesheet' media='{media}' type='text/css' />");
            options.HeadContent = builder.ToString();
        }


        /// <summary>
        /// Injects additional Javascript files into the index.html page
        /// </summary>
        /// <param name="options"></param>
        /// <param name="path">A path to the javascript - i.e. the script "src" attribute</param>
        /// <param name="type">The script type - i.e. the script "type" attribute</param>
        public static void InjectJavascript(this RapiDocOptions options, string path, string type = "text/javascript")
        {
            var builder = new StringBuilder(options.HeadContent);
            builder.AppendLine($"<script src='{path}' type='{type}'></script>");
            options.HeadContent = builder.ToString();
        }

        
        /// <summary>
        /// Sets RapiDoc JSON endpoint. Can be fully-qualified or relative to the UI page
        /// </summary>
        /// <param name="options"></param>
        /// <param name="url">Can be fully qualified or relative to the current host</param>
        /// <param name="name">The description that appears in the document selector drop-down</param>
        public static void SetRapidocEndpoint(this RapiDocOptions options, string url, string name)
        {
            options.SpecUrl = url;
            options.HeadingText = name;
        }
    }
}
