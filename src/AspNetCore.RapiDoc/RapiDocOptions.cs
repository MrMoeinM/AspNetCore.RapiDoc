using System;
using System.Drawing;

namespace AspNetCore.RapiDoc
{
    public class RapiDocOptions
    {
        /// <summary>
        /// Url of the OpenAPI spec to view	
        /// </summary>
        public string SpecUrl { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets a title for the rapidoc page
        /// </summary>
        public string DocumentTitle { get; set; } = "RapiDoc";


        /// <summary>
        /// To list tags in alphabetic order, otherwise tags will be ordered based on how it is specified under the tags section in the spec.
        /// </summary>
        public bool SortTags { get; set; } = false;


        /// <summary>
        /// Sort endpoints within each tag by path, method or summary.
        /// </summary>
        public EndpointType SortEndpointsBy { get; set; }


        /// <summary>
        /// Heading text on top-left corner	
        /// </summary>
        public string HeadingText { get; set; }


        /// <summary>
        /// Initial location on the document (identified by method and path) where you want to go after the spec is loaded.
        /// goto-path should be in the form of {method}-{path}
        /// for instance you want to scrollTo GET /user/login you should provide the location as get-/user/login
        /// </summary>
        public PathInfo GotoPath { get; set; }


        /// <summary>
        /// Request fields will be filled with example value (if provided in spec)
        /// </summary>
        public bool FillRequestFieldsWithExample { get; set; } = true;


        public UIColorsAndFontsOptions UIColorsAndFontsOptions { get; set; }

    }

    public class PathInfo
    {
        public PathInfo(SubmitMethod method, string path)
        {
            this.Method = method;
            this.Path = path;
        }

        public SubmitMethod Method { get; set; }
        public string Path { get; set; }
    }

    public enum EndpointType
    {
        Path,
        Method,
        Summary
    }

    public enum SubmitMethod
    {
        Get,
        Put,
        Post,
        Delete,
        Options,
        Head,
        Patch,
        Trace
    }

    public class UIColorsAndFontsOptions
    {
        /// <summary>
        /// Is the base theme, which is used for calculating colors for various UI components. 'theme', 'bg-color' and 'text-color' are the base attributes for generating a custom theme	
        /// </summary>
        public Theme Theme { get; set; } = Theme.Dark;


        /// <summary>
        /// Color for main background
        /// </summary>
        public Color BgColor { get; set; }


        /// <summary>
        /// Color for text
        /// </summary>
        public Color TextColor { get; set; }


        /// <summary>
        /// Color for the header's background
        /// </summary>
        public Color HeaderColor { get; set; }


        /// <summary>
        /// Color on various controls such as buttons, tabs
        /// </summary>
        public Color PrimaryColor { get; set; }


        /// <summary>
        /// RapiDoc will attempt to load fonts from CDN, if this is not intended, then set this to false.
        /// </summary>
        public bool LoadFonts { get; set; } = true;


        /// <summary>
        /// Font name(s) to be used for regular text
        /// </summary>
        public string RegularFont { get; set; }


        /// <summary>
        /// Font name(s) to be used for mono-spaced text
        /// </summary>
        public string MonoFont { get; set; }


        /// <summary>
        /// sets the relative font sizes for the entire document
        /// </summary>
        public FontSize FontSize { get; set; }
    }

    public enum Theme
    {
        Light,
        Dark
    }

    public enum FontSize
    {
        Default,
        Large,
        Largest
    }
}