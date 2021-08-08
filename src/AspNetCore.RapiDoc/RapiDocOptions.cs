using AspNetCore.RapiDoc.Util;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace AspNetCore.RapiDoc
{
    public class RapiDocOptions
    {
        /// <summary>
        ///  Gets or sets a route prefix for accessing the RapiDoc
        /// </summary>
        public string RoutePrefix { get; set; } = "rapidoc";

        /// <summary>
        /// Gets or sets a Stream function for retrieving the rapidoc page
        /// </summary>
        public Func<Stream> IndexStream { get; set; } = () => typeof(RapiDocOptions).GetTypeInfo().Assembly
            .GetManifestResourceStream("AspNetCore.RapiDoc.index.html");

        /// <summary>
        /// Url of the OpenAPI spec to view	
        /// </summary>
        public string SpecUrl { get; set; } = string.Empty;

        private string title = "RapiDoc";

        /// <summary>
        /// Heading text on top-left corner. Equivalent to <see cref="HeadingText"/>
        /// </summary>
        public string DocumentTitle
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }


        /// <summary>
        /// Heading text on top-left corner. Equivalent to <see cref="DocumentTitle"/>
        /// </summary>
        public string HeadingText
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }


        /// <summary>
        /// To list tags in alphabetic order, otherwise tags will be ordered based on how it is specified under the tags section in the spec.
        /// </summary>
        public bool SortTags { get; set; } = false;


        /// <summary>
        /// Sort endpoints within each tag by path, method or summary.
        /// </summary>
        public EndpointType SortEndpointsBy { get; set; } = EndpointType.Path;


        /// <summary>
        /// Gets or sets additional content to place in the head of the rapidoc page
        /// </summary>
        public string HeadContent { get; set; } = string.Empty;


        /// <summary>
        /// Initial location on the document (identified by method and path) where you want to go after the spec is loaded.
        /// goto-path should be in the form of {method}-{path}
        /// for instance you want to scrollTo GET /user/login you should provide the location as get-/user/login
        /// </summary>
        public PathInfo GotoPath { get; set; } = null;


        /// <summary>
        /// Request fields will be filled with example value (if provided in spec)
        /// </summary>
        public bool FillRequestFieldsWithExample { get; set; } = true;


        /// <summary>
        /// UI Colors and Fonts
        /// </summary>
        public UIColorsAndFontsOptions UIColorsAndFontsOptions { get; set; } = new UIColorsAndFontsOptions();


        /// <summary>
        /// Navigation bar settings (only applicable in read and focused render style)
        /// </summary>
        public NavigationBarOptions NavigationBarOptions { get; set; } = new NavigationBarOptions();


        /// <summary>
        /// UI Layout and Placement
        /// </summary>
        public UILayoutOptions UILayoutOptions { get; set; } = new UILayoutOptions();


        /// <summary>
        /// Hide/Show Sections
        /// </summary>
        public SectionOptions SectionOptions { get; set; } = new SectionOptions();


        /// <summary>
        /// API Server and calls
        /// </summary>
        public ApiServerOptions ApiServerOptions { get; set; } = new ApiServerOptions();

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

        public override string ToString()
        {
            return $"{Method.GetDisplayValue()}-{Path}";
        }
    }

    public enum EndpointType
    {
        [Display(Name = "path")]
        Path = 1,

        [Display(Name = "method")]
        Method = 2,

        [Display(Name = "summary")]
        Summary = 3
    }

    public enum SubmitMethod
    {
        [Display(Name = "get")]
        Get = 1,

        [Display(Name = "put")]
        Put = 2,

        [Display(Name = "post")]
        Post = 3,

        [Display(Name = "delete")]
        Delete = 4,

        [Display(Name = "options")]
        Options = 5,

        [Display(Name = "head")]
        Head = 6,

        [Display(Name = "patch")]
        Patch = 7,

        [Display(Name = "trace")]
        Trace = 8
    }

    #region
    public class UIColorsAndFontsOptions
    {
        /// <summary>
        /// Is the base theme, which is used for calculating colors for various UI components. 'theme', 'bg-color' and 'text-color' are the base attributes for generating a custom theme	
        /// </summary>
        public Theme Theme { get; set; } = Theme.Dark;


        /// <summary>
        /// Color for main background
        /// </summary>
        public Color BgColor { get; set; } = Color.Empty;


        /// <summary>
        /// Color for text
        /// </summary>
        public Color TextColor { get; set; } = Color.Empty;


        /// <summary>
        /// Color for the header's background
        /// </summary>
        public Color HeaderColor { get; set; } = Color.Empty;


        /// <summary>
        /// Color on various controls such as buttons, tabs
        /// </summary>
        public Color PrimaryColor { get; set; } = Color.Empty;


        /// <summary>
        /// RapiDoc will attempt to load fonts from CDN, if this is not intended, then set this to false.
        /// </summary>
        public bool LoadFonts { get; set; } = true;


        /// <summary>
        /// Font name(s) to be used for regular text
        /// </summary>
        public string RegularFont { get; set; } = string.Empty;


        /// <summary>
        /// Font name(s) to be used for mono-spaced text
        /// </summary>
        public string MonoFont { get; set; } = string.Empty;


        /// <summary>
        /// sets the relative font sizes for the entire document
        /// </summary>
        public FontSize FontSize { get; set; } = FontSize.Default;
    }

    public enum Theme
    {
        [Display(Name = "light")]
        Light = 1,

        [Display(Name = "dark")]
        Dark = 2
    }

    public enum FontSize
    {
        [Display(Name = "default")]
        Default = 1,

        [Display(Name = "large")]
        Large = 2,

        [Display(Name = "largest")]
        Largest = 3
    }
    #endregion

    #region NavigationBarOptions
    public class NavigationBarOptions
    {
        /// <summary>
        /// set true to show API paths in the navigation bar instead of summary/description 
        /// </summary>
        public bool UsePathInNavBar { get; set; } = false;


        /// <summary>
        /// Navigation bar's background color
        /// </summary>
        public Color NavBgColor { get; set; } = Color.Empty;


        /// <summary>
        /// URL of navigation bar's background image
        /// </summary>
        public string NavBgImage { get; set; } = string.Empty;


        /// <summary>
        /// Navigation bar's background image size (same as css background-size property) allowed values are
        /// </summary>
        public ImageSize NavBgImageSize { get; set; } = ImageSize.Auto;


        /// <summary>
        /// Navigation bar's background image repeat (same as css background-repeat property) allowed values are
        /// </summary>
        public Repeat NavBgImageRepeat { get; set; } = Repeat.NoRepeat; // ToDo: Find out it's default value


        /// <summary>
        /// Navigation bar's Text color	
        /// </summary>
        public Color NavTextColor { get; set; } = Color.Empty;


        /// <summary>
        /// Background color of the navigation item on mouse-over
        /// </summary>
        public Color NavHoverBgColor { get; set; } = Color.Empty;


        /// <summary>
        /// Text color of the navigation item on mouse-over	
        /// </summary>
        public Color NavHoverTextColor { get; set; } = Color.Empty;


        /// <summary>
        /// Current selected item indicator	
        /// </summary>
        public Color NavAccentColor { get; set; } = Color.Empty;


        /// <summary>
        /// Controls navigation item spacing	
        /// </summary>
        public Spacing NavItemSpacing { get; set; } = Spacing.Default;
    }

    public enum ImageSize
    {
        [Display(Name = "auto")]
        Auto = 1,

        [Display(Name = "length")]
        Length = 2,

        [Display(Name = "cover")]
        Cover = 3,

        [Display(Name = "contain")]
        Contain = 4,

        [Display(Name = "initial")]
        Initial = 5,

        [Display(Name = "inherit")]
        Inherit = 6
    }

    public enum Repeat
    {
        [Display(Name = "repeat")]
        Repeat = 1,

        [Display(Name = "repeat-x")]
        RepeatX = 2,

        [Display(Name = "repeat-y")]
        RepeatY = 3,

        [Display(Name = "no-repeat")]
        NoRepeat = 4,

        [Display(Name = "initial")]
        Initial = 5,

        [Display(Name = "inherit")]
        Inherit = 6
    }

    public enum Spacing
    {
        [Display(Name = "default")]
        Default = 1,

        [Display(Name = "compact")]
        Compact = 2,

        [Display(Name = "relaxed")]
        Relaxed = 3
    }
    #endregion

    #region UILayoutOptions
    public class UILayoutOptions
    {
        /// <summary>
        /// Layout helps in placement of request/response sections. In column layout, request and response sections are placed one below the other, In row layout they are placed side by side. This attribute is applicable only when the device width is more than 768px and the render-style is 'view'.
        /// </summary>
        public LayoutType Layout { get; set; } = LayoutType.Row;


        /// <summary>
        /// Determines display of api-docs. Currently there are three modes supported.
        /// <see cref="RenderStyle.View"/> friendly for quick exploring (expand/collapse the section of your interest) 
        /// <see cref="RenderStyle.Read"/> suitable for reading (like a continuous web-page)
        /// <see cref="RenderStyle.Focused"/> similar to read but focuses on a single endpoint at a time (good for large specs) 
        /// '<see cref="RenderStyle.Read"/>' - more suitable for reading '<see cref="RenderStyle.View"/>' more friendly for quick exploring
        /// </summary>
        public RenderStyle RenderStyle { get; set; } = RenderStyle.View;


        /// <summary>
        /// Applies only to focused render-style. It determinses the behavior of clicking on a Tag in navigation bar. It can either expand-collapse the tag or take you to the tag's description page.
        /// </summary>
        public NavAction OnNavTagClick { get; set; } = NavAction.ExpandCollapse;


        /// <summary>
        /// Two different ways to display object-schemas in the responses and request bodies
        /// </summary>
        public Schema SchemaStyle { get; set; } = Schema.Tree;

        /// <summary>
        /// Schemas are expanded by default, use this attribute to control how many levels in the schema should be expanded
        /// </summary>
        public int SchemaExpandLevel { get; set; } = 999;


        /// <summary>
        /// Constraint and descriptions information of fields in the schema are collapsed to show only the first line. Set it to true if you want them to fully expanded
        /// </summary>
        public bool SchemaDescriptionExpanded { get; set; } = false;


        /// <summary>
        /// Read-only fileds in request schemas is always hidden but are shown in response.
        /// If you do not want to hide read-only fields or hide them based on action you can configure this setting to 'never' or any combination of post | put | patch to indicate where to hide
        /// Schemas in response section is not affected by this setting.
        /// </summary>
        public ReadOnlyFieldHiddenInRequestSchema SchemaHideReadOnly { get; set; } = ReadOnlyFieldHiddenInRequestSchema.Always;


        /// <summary>
        /// Constraint and descriptions information of fields in the schema are collapsed to show only the first line. Set it to true if you want them to fully expanded
        /// </summary>
        public WriteOnlyFieldHiddenInResponseSchema SchemaHideWriteOnly { get; set; } = WriteOnlyFieldHiddenInResponseSchema.Always;


        /// <summary>
        /// The schemas are displayed in two tabs - Model and Example. This option allows you to pick the default tab that you would like to be active
        /// </summary>
        public TabSchema DefaultSchemaTab { get; set; } = TabSchema.Model;


        /// <summary>
        /// Valid css height value such as 400px, 50%, 60vh etc - Use this value to control the height of response textarea
        /// </summary>
        public string ResponseAreaHeight { get; set; } = "300px";
    }


    public enum LayoutType
    {
        [Display(Name = "row")]
        Row = 1,

        [Display(Name = "column")]
        Column = 2
    }

    public enum RenderStyle
    {
        [Display(Name = "view")]
        View = 1,

        [Display(Name = "read")]
        Read = 2,

        [Display(Name = "focused")]
        Focused = 3
    }

    public enum NavAction
    {
        [Display(Name = "expand-collapse")]
        ExpandCollapse = 1,

        [Display(Name = "show-description")]
        ShowDescription = 2
    }

    public enum Schema
    {
        [Display(Name = "tree")]
        Tree = 1,

        [Display(Name = "table")]
        Table = 2
    }

    public enum ReadOnlyFieldHiddenInRequestSchema
    {
        [Display(Name = "always")]
        Always = 1,

        [Display(Name = "never")]
        Never = 2,

        [Display(Name = "post")]
        Post = 3,

        [Display(Name = "put")]
        Put = 4,

        [Display(Name = "patch")]
        Patch = 5,

        [Display(Name = "post put")]
        PostPut = 6,

        [Display(Name = "post patch")]
        PostPatch = 7,

        [Display(Name = "put patch")]
        PutPatch = 8,

        [Display(Name = "post put patch")]
        PostPutPatch = 9
    }

    public enum WriteOnlyFieldHiddenInResponseSchema
    {
        [Display(Name = "always")]
        Always = 1,

        [Display(Name = "never")]
        Never = 2,
    }


    public enum TabSchema
    {
        [Display(Name = "model")]
        Model = 1,

        [Display(Name = "example")]
        Example = 2
    }
    #endregion

    #region SectionOptions
    public class SectionOptions
    {
        /// <summary>
        /// Show/Hide the documents info section
        /// Info section contains information about the spec, such as the title and description of the spec, the version, terms of services etc.In certain situation you may not need to show this section.For instance you are embedding this element inside a another help document. Chances are, the help doc may already have this info, in that case you may want to hide this section.
        /// </summary>
        public bool ShowInfo { get; set; } = true;


        /// <summary>
        /// Include headers from info -> description section to the Navigation bar (applies to read mode only)
        /// Will get the headers from the markdown in info - description (h1 and h2) into the menu on the left (in read mode) along with links to them. This option allows users to add navigation bar items using Markdown
        /// </summary>
        public bool InfoDescriptionHeadingsInNavbar { get; set; } = false;


        /// <summary>
        /// Show/Hide the components section both in document and menu (available only in focused render-style)
        /// Will show the components section along with schemas, responses, examples, requestBodies, headers, securitySchemes, links and callbacks Also will be shown in the menu on the left (in read mode)
        /// </summary>
        public bool ShowComponents { get; set; } = false;


        /// <summary>
        /// Show/Hide the header.
        /// If you do not want your user to open any other api spec, other than the current one, then set this attribute to false	
        /// </summary>
        public bool ShowHeader { get; set; } = true;


        /// <summary>
        /// Authentication feature, allows the user to select one of the authentication mechanism thats available in the spec. It can be http-basic, http-bearer or api-key. If you do not want your users to go through the authentication process, instead want them to use a pre-generated api-key then you may hide authentication section by setting this attribute to false and provide the api-key details using various api-key-???? attributes.	
        /// </summary>
        public bool AllowAuthentication { get; set; } = true;


        /// <summary>
        /// If set to 'false', user will not be able to load any spec url from the UI.	
        /// </summary>
        public bool AllowSpecUrlLoad { get; set; } = true;


        /// <summary>
        /// If set to 'false', user will not be able to load any spec file from the local drive. This attribute is applicable only when the device width is more than 768px, else this feature is not available	
        /// </summary>
        public bool AllowSpecFileLoad { get; set; } = true;


        /// <summary>
        /// Provides quick filtering of API	
        /// </summary>
        public bool AllowSearch { get; set; } = true;


        /// <summary>
        /// Provides advanced search functionality, to search through API-paths, API-description, API-parameters and API-Responses
        /// </summary>
        public bool AllowAdvancedSearch { get; set; } = true;


        /// <summary>
        /// The 'TRY' feature allows you to make REST calls to the API server. To disable this feature, set it to false.
        /// </summary>
        public bool AllowTry { get; set; } = true;


        /// <summary>
        /// If set to 'false', user will not be able to see or select API server (Server List will be hidden, however users will be able to see the server url near the 'TRY' button, to know in advance where the TRY will send the request). The URL specified in the server-url attribute will be used if set, else the first server in the API specification file will be used.	
        /// </summary>
        public bool AllowServerSelection { get; set; } = true;


        /// <summary>
        /// Allow or hide the ability to expand/collapse field descriptions in the schema
        /// </summary>
        public bool AllowSchemaDescriptionExpandToggle { get; set; } = true;

    }
    #endregion

    #region ApiServerOptions
    public class ApiServerOptions
    {
        /// <summary>
        /// OpenAPI spec has a provision for providing the server url. The UI will list all the server URLs provided in the spec. The user can then select one URL to which he or she intends to send API calls while trying out the apis. However, if you want to provide an API server of your own which is not listed in the spec, you can use this property to provide one. It is helpful in the cases where the same spec is shared between multiple environment say Dev and Test and each have their own API server.	
        /// </summary>
        public string ServerUrl { get; set; } = string.Empty;


        /// <summary>
        /// If you have multiple api-server listed in the spec, use this attribute to select the default API server, where all the API calls will goto. This can be changed later from the UI	
        /// </summary>
        public string DefaultApiServer { get; set; } = string.Empty;


        /// <summary>
        /// Name of the API key that will be send while trying out the APIs	
        /// </summary>
        public string ApiKeyName { get; set; } = string.Empty;


        /// <summary>
        /// determines how you want to send the api-key
        /// </summary>
        public ApiKeyLocation ApiKeyLocation { get; set; } = 0;


        /// <summary>
        /// Value of the API key that will be send while trying out the APIs. This can also be provided/overwritten from UI.	
        /// </summary>
        public string ApiKeyValue { get; set; } = string.Empty;


        /// <summary>
        /// Enables passing credentials/cookies in cross domain calls, as defined in the Fetch standard, in CORS requests that are sent by the browser
        /// </summary>
        public CrossDomainCookieBehaviour FetchCredentials { get; set; } = 0;
    }

    public enum ApiKeyLocation
    {
        [Display(Name = "header")]
        Header = 1,

        [Display(Name = "query")]
        Query = 2
    }

    public enum CrossDomainCookieBehaviour
    {
        /// <summary>
        /// Never send or receive cookies.
        /// </summary>
        [Display(Name = "omit")]
        Omit = 1,

        /// <summary>
        /// Send user credentials (cookies, basic http auth, etc..) if the URL is on the same origin as the calling script. This is the default value
        /// </summary>
        [Display(Name = "same-origin")]
        SameOrigin = 2,

        /// <summary>
        /// Always send user credentials (cookies, basic http auth, etc..), even for cross-origin calls.
        /// </summary>
        [Display(Name = "include")]
        Include = 3
    }
    #endregion
}