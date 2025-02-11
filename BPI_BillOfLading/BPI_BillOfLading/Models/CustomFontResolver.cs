using PdfSharp.Fonts;

public class CustomFontResolver : IFontResolver
{
    public byte[] GetFont(string faceName)
    {
        // Load font file bytes based on faceName
        string fontPath = FindFontFile(faceName);
        if (fontPath != null)
            return File.ReadAllBytes(fontPath);
        else
            return null; // Return null if font file not found
    }

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        string fontName = familyName.ToLowerInvariant();

        if (fontName == "thsarabun")
        {
            if (isBold && isItalic)
                return new FontResolverInfo("thsarabun-bolditalic");
            else if (isBold)
                return new FontResolverInfo("thsarabun-bold");
            else if (isItalic)
                return new FontResolverInfo("thsarabun-italic");
            else
                return new FontResolverInfo("thsarabun");
        }

        return null; // Return null if font not found
    }

    private string FindFontFile(string fontName)
    {
        // Specify the path to your font files
        string fontsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Fonts");

        // Map font names to actual font file names
        switch (fontName.ToLowerInvariant())
        {
            case "thsarabun":
                return Path.Combine(fontsDirectory, "THSarabun.ttf");
            case "thsarabun-bold":
                return Path.Combine(fontsDirectory, "THSarabun-Bold.ttf");
            case "thsarabun-italic":
                return Path.Combine(fontsDirectory, "THSarabun-Italic.ttf");
            case "thsarabun-bolditalic":
                return Path.Combine(fontsDirectory, "THSarabun-BoldItalic.ttf");
            default:
                return null; // Return null if font not found
        }
    }
}
