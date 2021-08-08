## General
 Property    | Description   | Default
------------ | ------------- | -------
RoutePrefix  | Route prefix for accessing the RapiDoc | "rapidoc"
SpecUrl      | Url of the OpenAPI spec to view | string.empty
HeadingText  | Heading text on top-left corner | "RapiDoc"
SortTags     | **Allowed:** *true* \| *false* <br /> To list tags in alphabetic order, otherwise tags will be ordered based on how it is specified under the tags section in the spec. | false
SortEndpointsBy | **Allowed:** *path* \| *method* \| *summary* <br /> Sort endpoints within each tag by path, method or summary \| [Example](https://mrin9.github.io/RapiDoc/examples/arrange-by-tags.html) | path
GotoPath | Initial location on the document (identified by method and path) where you want to go after the spec is loaded. <br /> for example if you want to scrollTo GET /user/login you should provide the location as ```new PathInfo(SubmitMethod.Get,"/user/login")``` | null
FillRequestFieldsWithExample | **Allowed:** *true* \| *false* <br /> Request fields will be filled with example value (if provided in spec)	 | true


## UI Colors and Fonts Options
 Property    | Description   | Default
------------ | ------------- | -------
Theme        | Is the base theme, which is used for calculating colors for various UI components. 'theme', 'bg-color' and 'text-color' are the base attributes for generating a custom theme | Dark
BgColor      | Color for main background | Dark Theme #333 <br /> Light Theme #fff
TextColor    | Color for text | Dark Theme #bbb <br /> Light Theme #444
HeaderColor  | Color for the header's background |  #444444
PrimaryColor | Color on various controls such as buttons, tabs |  #FF791A
LoadFonts    | RapiDoc will attempt to load fonts from CDN, if this is not intended, then set this to false. | true
RegularFont  | Font name(s) to be used for regular text | "Open Sans", Avenir, "Segoe UI", Arial, sans-serif
MonoFont     | Font name(s) to be used for mono-spaced text | Monaco, 'Andale Mono', 'Roboto Mono', 'Consolas' monospace
FontSize     | Sets the relative font sizes for the entire document | default

## Navigation Bar Options
 Property    | Description   | Default
------------ | ------------- | -------
UsePathInNavBar  | Set true to show API paths in the navigation bar instead of summary/description \| [Example](https://mrin9.github.io/RapiDoc/examples/nav-item-as-path.html)  | false
NavBgColor  | Navigation bar's background color [Example](https://mrin9.github.io/RapiDoc/examples/nav-bg-color.html) | 
NavBgImage  | URL of navigation bar's background [Example](https://mrin9.github.io/RapiDoc/examples/nav-bg-image.html) | string.empty
NavBgImageSize  | Navigation bar's background image size (same as css background-size property) | Auto
NavBgImageRepeat  | Navigation bar's background image repeat (same as css background-repeat property) | NoRepeat
NavTextColor  | Navigation bar's Text color	| no color
NavHoverBgColor  | Background color of the navigation item on mouse-over | no color
NavHoverTextColor  | Text color of the navigation item on mouse-over | no color
NavAccentColor  | Current selected item indicator | no color
NavItemSpacing  | Controls navigation item spacing [Example](https://mrin9.github.io/RapiDoc/examples/navbar-spacing.html) | Default

## UI Layout Options
 Property    | Description   | Default
------------ | ------------- | -------
Layout  | Layout helps in placement of request/response sections. In column layout, request and response sections are placed one below the other, In row layout they are placed side by side. This attribute is applicable only when the device width is more than 768px and the render-style is 'View'. | Row
RenderStyle  | Determines display of api-docs. Currently there are three modes supported <br /> **View** friendly for quick exploring (expand/collapse the section of your interest) <br /> **Read** suitable for reading (like a continuous web-page) <br /> **Focused** similar to read but focuses on a single endpoint at a time (good for large specs) <br /> **Read** more suitable for reading **View** more friendly for quick exploring | View
OnNavTagClick  | Applies only to focused render-style. It determinses the behavior of clicking on a Tag in navigation bar. It can either expand-collapse the tag or take you to the tag's description page. | ExpandCollapse
SchemaStyle  | Two different ways to display object-schemas in the responses and request bodies | Tree
SchemaExpandLevel  | Schemas are expanded by default, use this attribute to control how many levels in the schema should be expanded | 999
SchemaDescriptionExpanded  | Constraint and descriptions information of fields in the schema are collapsed to show only the first line. Set it to true if you want them to fully expanded | false
SchemaHideReadOnly  | Read-only fileds in request schemas is always hidden but are shown in response. <br /> If you do not want to hide read-only fields or hide them based on action you can configure this setting to 'never' or any combination of post \| put \| patch to indicate where to hide <br /> Schemas in response section is not affected by this setting. | Always
SchemaHideWriteOnly  | Constraint and descriptions information of fields in the schema are collapsed to show only the first line. Set it to true if you want them to fully expanded | Always
DefaultSchemaTab  | The schemas are displayed in two tabs - Model and Example. This option allows you to pick the default tab that you would like to be active | Model
ResponseAreaHeight  | Valid css height value such as 400px, 50%, 60vh etc - Use this value to control the height of response textarea | "300px"

## Section Options
 Property    | Description   | Default
------------ | ------------- | -------
ShowInfo  | Show/Hide the documents info section <br /> Info section contains information about the spec, such as the title and description of the spec, the version, terms of services etc.In certain situation you may not need to show this section.For instance you are embedding this element inside a another help document. Chances are, the help doc may already have this info, in that case you may want to hide this section. | true
InfoDescriptionHeadingsInNavbar  | Include headers from info -> description section to the Navigation bar (applies to read mode only) <br /> Will get the headers from the markdown in info - description (h1 and h2) into the menu on the left (in read mode) along with links to them. This option allows users to add navigation bar items using Markdown | false
ShowComponents  | Show/Hide the components section both in document and menu (available only in focused render-style) <br /> Will show the components section along with schemas, responses, examples, requestBodies, headers, securitySchemes, links and callbacks Also will be shown in the menu on the left (in read mode) | false
ShowHeader  | Show/Hide the header. <br /> If you do not want your user to open any other api spec, other than the current one, then set this attribute to false	 | true
AllowAuthentication  | Authentication feature, allows the user to select one of the authentication mechanism thats available in the spec. It can be http-basic, http-bearer or api-key. If you do not want your users to go through the authentication process, instead want them to use a pre-generated api-key then you may hide authentication section by setting this attribute to false and provide the api-key details using various api-key-???? attributes         | true
AllowSpecUrlLoad  | If set to 'false', user will not be able to load any spec url from the UI | true
AllowSpecFileLoad  | If set to 'false', user will not be able to load any spec file from the local drive. This attribute is applicable only when the device width is more than 768px, else this feature is not available	 | true
AllowSearch  | Provides quick filtering of API	 | true
AllowAdvancedSearch  | Provides advanced search functionality, to search through API-paths, API-description, API-parameters and API-Responses | true
AllowTry  | The 'TRY' feature allows you to make REST calls to the API server. To disable this feature, set it to false. | true
AllowServerSelection  | If set to 'false', user will not be able to see or select API server (Server List will be hidden, however users will be able to see the server url near the 'TRY' button, to know in advance where the TRY will send the request). The URL specified in the server-url attribute will be used if set, else the first server in the API specification file will be used.	 | true
AllowSchemaDescriptionExpandToggle  | Allow or hide the ability to expand/collapse field descriptions in the schema | true


## Api Server Options
 Property    | Description   | Default
------------ | ------------- | -------
ServerUrl        | OpenAPI spec has a provision for providing the server url. The UI will list all the server URLs provided in the spec. The user can then select one URL to which he or she intends to send API calls while trying out the apis. However, if you want to provide an API server of your own which is not listed in the spec, you can use this property to provide one. It is helpful in the cases where the same spec is shared between multiple environment say Dev and Test and each have their own API server.	 | string.empty
DefaultApiServer | If you have multiple api-server listed in the spec, use this attribute to select the default API server, where all the API calls will goto. This can be changed later from the UI | string.empty
ApiKeyName       | Name of the API key that will be send while trying out the APIs	 | string.empty
ApiKeyLocation   | Determines how you want to send the api-key | not set
ApiKeyValue      | Value of the API key that will be send while trying out the APIs. This can also be provided/overwritten from UI | string.empty
FetchCredentials | Enables passing credentials/cookies in cross domain calls, as defined in the Fetch standard, in CORS requests that are sent by the browser | not set
