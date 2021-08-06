using System;
using System.Drawing;

namespace AspNetCore.RapiDoc.Util
{
    public static class ColorConverter
    {
        public static string ToHtml(this Color color)
        {
            if (color.IsEmpty)
                return String.Empty;

            if (color.A == byte.MaxValue)
                if (
                        (color.R & 0x0F) == ((color.R >> 4) & 0x0F) &&
                        (color.G & 0x0F) == ((color.G >> 4) & 0x0F) &&
                        (color.B & 0x0F) == ((color.B >> 4) & 0x0F)
                    )
                {
                    return $"#{(color.R & 0x0f):x}{(color.G & 0x0f):x}{(color.B & 0x0f):x}";
                }
                else
                {
                    return $"#{color.R:x2}{color.G:x2}{color.B:x2}";
                }
            else
                return $"#{color.A:x2}{color.R:x2}{color.G:x2}{color.B:x2}";
        }

        public static Color FromHtml(this string htmlColor)
        {
#if NETSTANDARD2_0
            return ParseHtmlColor(htmlColor);
#else
            return ColorTranslator.FromHtml(htmlColor);
#endif
        }

#if NETSTANDARD2_0
        private static Color ParseHtmlColor(string htmlColor) => Color.FromArgb(HtmlColorToArgb(htmlColor));

        private static int HtmlColorToArgb(string htmlColor, bool requireHexSpecified = false, int defaultAlpha = 0xFF)
        {

            if (string.IsNullOrEmpty(htmlColor))
            {
                throw new ArgumentNullException(nameof(htmlColor));
            }

            if (!htmlColor.StartsWith("#") && requireHexSpecified)
            {
                throw new ArgumentException($"Provided parameter '{htmlColor}' is not valid");
            }

            htmlColor = htmlColor.TrimStart('#');


            // int[] symbols 
            var symbolCount = htmlColor.Length;
            var value = int.Parse(htmlColor, System.Globalization.NumberStyles.HexNumber);
            switch (symbolCount)
            {
                case 3: // RGB short hand
                    {
                        return defaultAlpha << 24
                            | (value & 0xF)
                            | (value & 0xF) << 4
                            | (value & 0xF0) << 4
                            | (value & 0xF0) << 8
                            | (value & 0xF00) << 8
                            | (value & 0xF00) << 12
                            ;
                    }
                case 4: // RGBA short hand
                    {
                        // Inline alpha swap
                        return (value & 0xF) << 24
                               | (value & 0xF) << 28
                               | (value & 0xF0) >> 4
                               | (value & 0xF0)
                               | (value & 0xF00)
                               | (value & 0xF00) << 4
                               | (value & 0xF000) << 4
                               | (value & 0xF000) << 8
                               ;
                    }
                case 6: // RGB complete definition
                    {
                        return defaultAlpha << 24 | value;
                    }
                case 8: // RGBA complete definition
                    {
                        // Alpha swap
                        return (value & 0xFF) << 24 | (value >> 8);
                    }
                default:
                    throw new FormatException("Invalid HTML Color");
            }
        }
#endif
    }
}
